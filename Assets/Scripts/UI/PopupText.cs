using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupText : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(DisablePopupText());
    }

    IEnumerator DisablePopupText()
    {
        yield return Managers.Coroutine.WaitForSecondsEx(1f);
        Managers.Game.Destroy(this.GetComponent<Poolable>());
    }
}
