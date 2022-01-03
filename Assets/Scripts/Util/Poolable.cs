using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poolable : MonoBehaviour
{
    public Transform myTransform;
    public GameObject myGameObject;
    public bool isUsing;

    public void Init()
    {
        myTransform = GetComponent<Transform>();
        myGameObject = gameObject;
    }
}
