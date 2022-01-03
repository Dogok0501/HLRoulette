using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_DanceButton : BaseInteractable
{
    Vector3 buttonDeactivePosition = new Vector3(0f, -2f, -1.0f);

    public override IEnumerator SelectObject()
    {
        Managers.Sound.PlaySFX(Define.SFX.DanceButtonClick);

        transform.parent.GetComponentInChildren<Animator>().SetBool("isDance", true);
        Managers.Sound.StopBGM(Define.BGM.Main);
        Managers.Sound.PlayBGM(Define.BGM.Room);

        float elapsedTime = 0f;
        float waitTime = 3f;

        while (elapsedTime < waitTime)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, buttonDeactivePosition, (elapsedTime / waitTime));

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = buttonDeactivePosition;
    }
}
