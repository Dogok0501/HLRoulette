using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoamingFigureSpawner : MonoBehaviour
{
    List<int> figureIndexList;
    Figure generatedFigure;

    DefaultFigureBuilder defaultFigureBuilder;

    [SerializeField] Transform spawnPos;

    Vector3 figureLocalPosition = new Vector3(0f, 0.6f, 0f);
    Quaternion figureLocalRotation = new Quaternion(0f, 0f, 0f, 0f);

    private void Awake()
    {
        defaultFigureBuilder = new DefaultFigureBuilder();

        figureIndexList = new List<int>();
        for (int i = 1008; i < 1015; i++)
        {
            if (Array.IndexOf(Managers.Game.collectedFigureIndex, i) > -1)
                figureIndexList.Add(i);
        }

        GetFigureFromInventory();
    }

    public void GetFigureFromInventory()
    {
        if (figureIndexList.Count == 0)
            return;
        else
        {
            int figureIndex = figureIndexList[UnityEngine.Random.Range(0, figureIndexList.Count)];
            GenerateFigure(figureIndex);
        }

        
    }

    void GenerateFigure(int index)
    {
        generatedFigure = Managers.Game.Instantiate(index , spawnPos);
        generatedFigure.gameObject.name = generatedFigure.name;
        generatedFigure.figureBuilder = defaultFigureBuilder;
        generatedFigure.figureBuilder.FigureOnEnable(generatedFigure, index);

        generatedFigure.gameObject.AddComponent<RoamingFigureController>();
        generatedFigure.gameObject.AddComponent<NavMeshAgent>();

        generatedFigure.myTransform.localPosition = figureLocalPosition;
        generatedFigure.myTransform.localRotation = figureLocalRotation;

        generatedFigure.myTransform.localScale = new Vector3(generatedFigure.myTransform.localScale.x * 2.0f, generatedFigure.myTransform.localScale.y * 2.0f, generatedFigure.myTransform.localScale.z * 2.0f);
    }
}
