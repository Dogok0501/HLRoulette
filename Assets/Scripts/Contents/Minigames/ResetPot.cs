using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPot : MonoBehaviour
{
    Animator potAnim;
    Minigame_Pot potPanel;

    private void Start()
    {
        potAnim = GetComponent<Animator>();
        potPanel = GetComponentInParent<Minigame_Pot>();
    }

    private void ResetPotPanel()
    {
        if(!potAnim.GetBool("is Game Success"))
        {
            potPanel.life -= 1;
        }

        this.transform.parent.gameObject.SetActive(false);
        this.transform.parent.gameObject.SetActive(true);
        potAnim.SetTrigger("To Pot Bonk");
    }
}
