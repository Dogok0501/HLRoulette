using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StandStates : MonoBehaviour
{
    DefaultState _defaultState;
    public DefaultState defaultState { get { return _defaultState; } }

    BallSpawnState _ballSpawnState;
    public BallSpawnState ballSpawnState { get { return _ballSpawnState; } }

    BallOpenState _ballOpenState;
    public BallOpenState ballOpenState { get { return _ballOpenState; } }

    FigureState _figureState;
    public FigureState figureState { get { return _figureState; } }

    FigureSellState _figureSellState;
    public FigureSellState figureSellState { get { return _figureSellState; } }

    public IStandSequence currentState;

    ParticleSystem _firework;
    public ParticleSystem firework { get { return _firework; } }

    ParticleSystem _circle;
    public ParticleSystem circle { get { return _circle; } }

    public TextMeshProUGUI figureNameTM;

    //Inspector Drag and Drop
    public GameObject RarityStars;
    public Canvas figureNameCanvas;

    void Awake()
    {
        GetComponentInit();
        GetParticleSystem();
    }

    private void GetComponentInit()
    {
        _defaultState = new DefaultState(this);
        _ballSpawnState = new BallSpawnState(this);
        _ballOpenState = new BallOpenState(this);
        _figureState = new FigureState(this);
        _figureSellState = new FigureSellState(this);

        figureNameTM = figureNameCanvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void GetParticleSystem()
    {
        _firework = transform.GetChild(1).GetComponent<ParticleSystem>();
        _circle = transform.GetChild(2).GetComponent<ParticleSystem>();

        firework.Stop();
        circle.Stop();
    }    

    void Start()
    {
        currentState = defaultState;

        for (int i = 0; i < RarityStars.transform.childCount; i++)
        {
            RarityStars.transform.GetChild(i).gameObject.SetActive(false);
        }

        figureNameCanvas.gameObject.SetActive(false);
        StandLoop();
    }    

    private void StandLoop()
    {
        StartCoroutine(UpdateStand());
    }

    private IEnumerator UpdateStand()
    {
        while(!Managers.Game.isGameOver)
        {
            currentState.UpdateAction();
            yield return null;
        }
    }

    public int CountChildren()
    {
        int children;

        children = transform.childCount;

        return children;
    }
}
