using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyHoldText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI creditHoldText;

    private void OnEnable()
    {
        MoneyTextUpdate();
    }

    void MoneyTextUpdate()
    {
        creditHoldText.text = Managers.Game.moneyHold.ToString();
    }

    //void Start()
    //{
    //    Init();
    //    MoneyUpdate();
    //}

    //private void Init()
    //{

    //}

    //private void MoneyUpdate()
    //{
    //    StartCoroutine(MoneyTextUpdate());
    //}

    //private IEnumerator MoneyTextUpdate()
    //{
    //    while (!Managers.Game.isGameOver)
    //    {
    //        moneyHoldText.text = "Money : " + Managers.Game.moneyHold.ToString();

    //        yield return null;
    //    }
    //    if (Managers.Game.isGameOver)
    //        yield break;
    //}

}
