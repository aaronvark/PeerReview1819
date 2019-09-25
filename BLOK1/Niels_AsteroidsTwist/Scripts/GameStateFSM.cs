using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void GameStateEvent(IGameState state);

public interface IGameState
{
    GameStateEvent OnStateSwitch { get; set; }
    void Start();
    void Run();
    void Complete();
}

public class GameStateFSM : MonoBehaviour
{
    IGameState currentState;

    public GameStateFSM(IGameState firstState)
    {
        SwitchState(firstState);
    }

    public void Start()
    {
        SwitchState(new GStart());
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.Run();
            Debug.Log(currentState);
        }
    }

    public void SwitchState(IGameState _newState)
    {
        //clean up
        if (currentState != null)
        {
            currentState.OnStateSwitch -= SwitchState;
            currentState.Complete();
        }

        //initialize
        _newState.Start();
        _newState.OnStateSwitch += SwitchState;

        //store current
        currentState = _newState;
    }
}

public class GStart : IGameState
{
    public GameStateEvent OnStateSwitch { get; set; }

    public void Start()
    {
        UIManager.Instance.startMenu.SetActive(true);
        UIManager.Instance.EnableMenu(UIManager.Instance.ingameUI);
        UIManager.Instance.EnableMenu(UIManager.Instance.gameOverMenu);

    }

    public void Run()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    OnStateSwitch(new GRunning());
        //    Debug.Log("ASLEEP");
        //}
    }

    public void Complete()
    {
        UIManager.Instance.EnableMenu(UIManager.Instance.startMenu);
    }
}

public class GRunning : IGameState
{
    public GameStateEvent OnStateSwitch { get; set; }

    public void Start()
    {
        UIManager.Instance.EnableMenu(UIManager.Instance.ingameUI);
        GameManager.Instance.gameIsPlaying = true;
    }

    public void Run()
    {
        //timer += Time.deltaTime;
        //if (timer > 10)
        //{
        //    OnStateSwitch(new GEndGame());
        //    Debug.Log("AWAKEKE");
        //}
    }

    public void Complete()
    {
        UIManager.Instance.EnableMenu(UIManager.Instance.ingameUI);
        GameManager.Instance.gameIsPlaying = false;
    }
}

public class GEndGame : IGameState
{
    public GameStateEvent OnStateSwitch { get; set; }

    public void Start()
    {
        UIManager.Instance.EnableMenu(UIManager.Instance.gameOverMenu);
    }

    public void Run()
    {
        //timer += Time.deltaTime;
        //if (timer > 10)
        //{
        //    OnStateSwitch(new GStart());
        //    Debug.Log("AWAKEKE");
        //}
    }

    public void Complete()
    {
        UIManager.Instance.EnableMenu(UIManager.Instance.gameOverMenu);
    }
}
