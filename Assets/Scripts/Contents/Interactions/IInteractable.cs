using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void trueReach();

    void falseReach();

    void Interact();

    IEnumerator ToSelectScene();

    IEnumerator EmissioionLerp();
}
