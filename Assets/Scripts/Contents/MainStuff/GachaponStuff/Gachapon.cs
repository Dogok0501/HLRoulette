using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using TMPro;

public class Gachapon : MonoBehaviour
{
    MainScene mainScene;
    Camera mainCamera;

    Figure[] generatedFigure = new Figure[Define.GACHA_POP_NUM];
    GameObject ballPrefab;
    GameObject popupTextPrefab;

    [SerializeField] GameObject defaultCamPos;
    [SerializeField] GameObject leverCamPos;
    [SerializeField] GameObject gachaCamPos;

    [SerializeField] GameObject confirmButtonCanvas;

    [SerializeField] Light directionalLight;

    [SerializeField] GameObject mainUIPanel;

    bool[] isCollected;

    private void Start()
    {
        Init();
        CreatePool();
        GameLoop();
    }

    private void Init()
    {
        mainScene = FindObjectOfType<MainScene>();
        mainCamera = Camera.main.GetComponent<Camera>();
        ballPrefab = Managers.Resource.GetBallPrefab();
        popupTextPrefab = Managers.Resource.GetTextPrefab();
        isCollected = new bool[4];
    }

    private void CreatePool()
    {
        //Figure Pool
        for (int i = 0; i < Managers.Data.figureDataDict.Count; i++)
        {
            Managers.Pool.CreatePool(i + 1000);
        }               

        //Ball Pool
        Managers.Pool.CreatePool(ballPrefab);

        //Text Pool
        Managers.Pool.CreatePool(popupTextPrefab);
    }

    private void GameLoop()
    {
        StartCoroutine(GachaGacha());
    }

    IEnumerator GachaGacha()
    {        
        while (!Managers.Game.isGachaing && !Managers.Game.isGameOver)
        {
            if(Managers.Game.isPull)
            {
                mainUIPanel.SetActive(false);
                Managers.Game.gachaStack += Define.GACHA_POP_NUM;
                Managers.SaveLoad.UpdateGachaStack(Managers.Game.gachaStack);
                StartCoroutine(LerpDirectionalLightIntensity(Define.DEFAULT_DIRECTIONAL_LIGHT_INTENSITY, 0f, 4f));
                StartCoroutine(PayMoney());
            }
            yield return null;
        }        
    }

    private IEnumerator PayMoney()
    {
        if (Managers.Game.moneyHold >= Define.DEFAULT_GACHA_COST)
        {
            isCollected = Enumerable.Repeat(false, 4).ToArray();
            Managers.Game.moneyHold -= Define.DEFAULT_GACHA_COST;
            Managers.SaveLoad.UpdateMoneyHold(Managers.Game.moneyHold);

            StartCoroutine(GachaCinema());

            for (int i = 0; i < Define.GACHA_POP_NUM; i++)
            {
                SpawnBall(i);
                yield return Managers.Coroutine.WaitForSecondsEx(0.2f);
            }
        }
        else
            yield return null;
    }

    //가챠연출

    private IEnumerator GachaCinema() 
    {
        Managers.Game.isGachaing = true;

        mainCamera.transform.GetChild(0).gameObject.SetActive(true); //RaycastBlocker active

        StartCoroutine(Shake(mainCamera, defaultCamPos.transform.position, 4f, 0.15f));               
        StartCoroutine(MoveLerpGameObject(mainCamera.gameObject, leverCamPos.transform.position, 4f)); //leverCamPos로 줌인
        yield return Managers.Coroutine.WaitForSecondsEx(4f);

        StartCoroutine(MoveLerpGameObject(mainCamera.gameObject, gachaCamPos.transform.position, 1f)); //GachaCamPos로 줌아웃
        yield return Managers.Coroutine.WaitForSecondsEx(1.5f);

        StartCoroutine(CameraMoveInOrder());
        yield return null;
    }    

    private void SpawnBall(int standNum)
    {
        Poolable ball = Managers.Game.Instantiate(ballPrefab, mainScene.stand[standNum]);
        ball.myTransform.localPosition = new Vector3(0, 10f, 0);

        StartCoroutine(MoveLerpGameObject(ball.gameObject, new Vector3(0, 1.5f, 0), 0.25f, 5f, Define.SFX.GachaDrop));
        StartCoroutine(Shake(mainCamera, gachaCamPos.transform.position, 0.5f, 0.15f, 5.25f));
        
        SellorCollectFigure(ball.GetComponent<SpawnFigure>().GetFigureIndex(), standNum);

    }

    private void SellorCollectFigure(int figureIndex, int standNum)
    {
        if (Array.IndexOf(Managers.Game.collectedFigureIndex, figureIndex) == -1)
        {
            List<int> tempList = Managers.Game.collectedFigureIndex.ToList();
            tempList.Add(figureIndex);
            Managers.Game.collectedFigureIndex = tempList.ToArray();

            Managers.SaveLoad.UpdateCollectedFigureIndex(Managers.Game.collectedFigureIndex);

            isCollected[standNum] = false;
        }
        else
        {
            isCollected[standNum] = true;

            Managers.Game.moneyHold += Managers.Data.figureDataDict[figureIndex].sell_earn;
            Managers.SaveLoad.UpdateMoneyHold(Managers.Game.moneyHold);
        }
    }

    private IEnumerator CameraMoveInOrder()
    {
        for (int i = 0; i < Define.GACHA_POP_NUM; i++)
        {
            float elapsedTime = 0f;
            float duration = 1f;

            Managers.Game.isDisplayFigureInfo = true;          

            while (mainScene.stand[i].GetChild(6).CompareTag("Ball"))
            {
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, mainScene.stand[i].GetChild(0).transform.position, (elapsedTime / duration));
                elapsedTime += Time.deltaTime;

                yield return null;
            }

            mainCamera.transform.position = mainScene.stand[i].GetChild(0).transform.position;

            //피규어 정보 등급 일러스트 보여줌. 다시 터치를 해야 이후로 넘어가게
            while (Managers.Game.isDisplayFigureInfo)
            {
                yield return null;
            }

            if (i == Define.GACHA_POP_NUM - 1)
            {
                confirmButtonCanvas.SetActive(true);
                StartCoroutine(MoveLerpGameObject(mainCamera.gameObject, gachaCamPos.transform.position, 0.5f));
            }                
        }
    }    

    //팔고 다시 처음으로 돌아가기

    public void GachaConfirm()
    {
        if(!Managers.Game.isGameOver)
        {
            Managers.Game.isPull = false;
            GetGeneratedFigure();
            BackToDefaultCamPos();
            StartCoroutine(ClearGeneratedFigure());
            confirmButtonCanvas.SetActive(false);
            StartCoroutine(LerpDirectionalLightIntensity(0f, Define.DEFAULT_DIRECTIONAL_LIGHT_INTENSITY, 3f));
        }
    }

    private void GetGeneratedFigure()
    {
        for (int i = 0; i < Define.GACHA_POP_NUM; i++)
        {
            generatedFigure[i] = mainScene.stand[i].GetChild(6).GetComponent<Figure>();
        }
    }

    private void BackToDefaultCamPos()
    {
        mainCamera.transform.GetChild(0).gameObject.SetActive(false); //RaycastBlocker deactive
        StartCoroutine(MoveLerpGameObject(mainCamera.gameObject, defaultCamPos.transform.position, 0.5f, 3.5f));
    }        

    private IEnumerator ClearGeneratedFigure()
    {
        Managers.Game.isGachaing = false;
        StartCoroutine(GachaGacha());

        if (generatedFigure != null)
        {
            for (int i = 0; i < 4; i++)
            {
                //디졸브
                if (generatedFigure[i].figureItem.rarity != "Normal")
                    StartCoroutine(generatedFigure[i].GetComponent<DissolveHelper>().Dissolving(0.0030f));
                              
                yield return Managers.Coroutine.WaitForSecondsEx(0.75f);

                //보유한 인덱스에 있다면 팔린 가격 텍스트 팝업 뿅, 없다면 New! 텍스트 팝업 뿅
                Poolable popup = Managers.Game.Instantiate(popupTextPrefab, mainScene.stand[i]);

                if(isCollected[i])
                {
                    popup.GetComponent<TextMeshPro>().text = "+" + generatedFigure[i].figureItem.sell_earn.ToString();
                    popup.transform.localPosition = new Vector3(0f, 1.6f, 0.5f);
                }
                else
                {
                    popup.GetComponent<TextMeshPro>().text = "New!";
                    popup.transform.localPosition = new Vector3(0f, 1.6f, 0.5f);
                }

                Managers.Sound.PlaySFX(Define.SFX.SellEarn);
                Managers.Game.Destroy(generatedFigure[i]);
            }
            mainUIPanel.SetActive(true);
        }
        else
            yield return null;
    }

    private IEnumerator MoveLerpGameObject(GameObject gameObject, Vector3 lerpPos, float duration, float waitTime = 0, Define.SFX sfx = Define.SFX.None)
    {
        yield return Managers.Coroutine.WaitForSecondsEx(waitTime);

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            gameObject.transform.localPosition = Vector3.Lerp(gameObject.transform.localPosition, lerpPos, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        gameObject.transform.localPosition = lerpPos;

        Managers.Sound.PlaySFX(sfx);
    }

    IEnumerator Shake(Camera cam, Vector3 originalPosition, float duration, float magnitude, float waitTime = 0)
    {
        yield return Managers.Coroutine.WaitForSecondsEx(waitTime);

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float x = UnityEngine.Random.Range(-0.5f, 0.5f) * magnitude;
            float y = UnityEngine.Random.Range(-0.5f, 0.5f) * magnitude;

            cam.transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }

    IEnumerator LerpDirectionalLightIntensity(float start, float end, float duration)
    {
        float elapsedTime = 0f;

        while(elapsedTime < duration)
        {
            directionalLight.intensity = Mathf.Lerp(start, end, (elapsedTime / duration));

            elapsedTime += Time.deltaTime;

            yield return null;
        }
        directionalLight.intensity = end;
    }
}
