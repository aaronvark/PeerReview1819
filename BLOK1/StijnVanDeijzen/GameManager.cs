using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    public static float blockSpeed = 10;
    public static float middleForce = 100;

    //TODO: set rotate speed in animator
    //public float middleRotateSpeed = 1;

    public Player[] players = new Player[2];

    public UI UI;

    public delegate void AttractDelegate(Vector3 target, float force);
    public AttractDelegate Attract;

    public System.Action UpdateGame;

    public GameStateMachine gameStateMachine;
    private bool gameInitialized = false;

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
        gameStateMachine = new GameStateMachine(this, new MenuState());
        players[0] = new Player(new Vector3(-17, 0, 0));
        players[1] = new Player(new Vector3(17, 0, 0));
    }

    private void OnDestroy()
    {
        Attract = null;
    }

    private void Update()
    {
        gameStateMachine.Update();
    }

    public void InitializeGame()
    {
        if (!gameInitialized)
        {
            BlockPool.Initialize(this);

            players[0].NewBlock();
            players[1].NewBlock();

            UI = GameObject.FindObjectOfType<UI>();//TODO: improve
            gameInitialized = true;
        }
    }

    public void AddScore(float x, int count)
    {
        Debug.Log("Added Score");
        int addscore = Mathf.RoundToInt(Mathf.Pow(10, count));
        if (x < 0)
        {
            players[0].AddScore(addscore);
        }
        else
        {
            players[1].AddScore(addscore);
        }
    }


}
