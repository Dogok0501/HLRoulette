using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figure : MonoBehaviour
{
    public FigureItem figureItem;
    public Transform myTransform;
    public GameObject myGameObject;
    public IFigureBuilder figureBuilder;

    public void Init()
    {
        myTransform = GetComponent<Transform>();
        myGameObject = gameObject;
    }
}
