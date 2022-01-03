using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame_Pot : MonoBehaviour
{
    [SerializeReference] MinigameScene minigameScene;
    [SerializeReference] Minigame_Gameover gameoverPanel;

    [SerializeReference] GameObject pot;
    Animator potAnim;
    Animator anim;

    [SerializeReference] GameObject drugs;
    [SerializeReference] List<GameObject> minigames = new List<GameObject>();
    [SerializeReference] Canvas minigameCanvas;

    GameObject[] drug = new GameObject[4];

    public int life = Define.MINIGAME_LIFE;

    private void Awake()
    {
        potAnim = pot.GetComponent<Animator>();
        anim = GetComponent<Animator>();

        for (int i = 0; i < drugs.transform.childCount; i++)
        {
            drug[i] = drugs.transform.GetChild(i).gameObject;
        }
    }

    private void OnEnable()
    {
        drugs.SetActive(true);

        if(life == Define.MINIGAME_LIFE)
        {
            StartCoroutine(ToPotBonk());
        }
        else if(life != Define.MINIGAME_LIFE && life != 0)
        {
            drug[4 - life].SetActive(false);
            StartCoroutine(ToPotBonk());
        }
        else if(life == 0)
        {            
            minigameScene.isMinigameOver = true;
            gameoverPanel.gameObject.SetActive(true);
            this.gameObject.SetActive(false);

            minigameScene.UpdateMoney();
        }
    }

    IEnumerator ToPotBonk()
    {
        yield return Managers.Coroutine.WaitForSecondsEx(2f);
        potAnim.SetTrigger("To Pot Bonk");

        yield return Managers.Coroutine.WaitForSecondsEx((62 / 60));
        StartCoroutine(RandomGameActive());

        anim.SetBool("Zoom", true);
        drugs.SetActive(false);
        potAnim.SetBool("is Game End", false);
    }

    private IEnumerator RandomGameActive()
    {
        int randNum = Random.Range(0, minigames.Count);
        minigames[randNum].SetActive(true);
        yield return Managers.Coroutine.WaitForSecondsEx(0.2f);
        yield return null;
        minigameCanvas.sortingOrder = 1;
    }
}
