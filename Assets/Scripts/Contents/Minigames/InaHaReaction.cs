using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InaHaReaction : MonoBehaviour
{
    Animator anim;

    private void OnEnable()
    {
        anim = GetComponentInParent<Animator>();

        if(anim.GetBool("is Game Success"))
            Managers.Sound.PlaySFX(Define.SFX.Minigame_Good);
        else if(!anim.GetBool("is Game Success"))
            Managers.Sound.PlaySFX(Define.SFX.Minigame_Bad);
    }
}
