using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Inventory : BaseInteractable
{
    [SerializeField] Canvas inventoryCanvas;
    [SerializeField] Canvas UICanvas;

    public override IEnumerator SelectObject()
    {
        Managers.Sound.PlaySFX(Define.SFX.Click);
        UICanvas.gameObject.SetActive(false);
        inventoryCanvas.gameObject.SetActive(true);

        yield return null;
    }    
}
