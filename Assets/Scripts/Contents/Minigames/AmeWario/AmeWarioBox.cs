using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmeWarioBox : MonoBehaviour
{
    AmeWario ameWario;
    BoxCollider2D boxCollider2D;
    Animator anim;

    private void OnEnable()
    {
        ameWario = GetComponentInParent<AmeWario>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        boxCollider2D.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Groundpound Hitbox")
        {
            Managers.Sound.PlaySFX(Define.SFX.Ame_Groundpound);
            anim.SetTrigger("BreakTri");
            ameWario.BoxBreak();
            boxCollider2D.enabled = false;
        }
    }
}
