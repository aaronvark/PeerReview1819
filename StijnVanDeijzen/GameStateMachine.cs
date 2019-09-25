//gebaseerd op Aaron's FSM voorbeeld


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public delegate void SimpleStateEvent(IGameState state, string sceneName = null);
public class GameStateMachine
{
    private IGameState beginState;
    private IGameState currentState;
    private IGameState NextState;
    private GameManager gameManager;
    public GameStateMachine(GameManager _gameManager, IGameState _beginState)
    {
        gameManager = _gameManager;
        beginState = _beginState;
        SwitchState(beginState);
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.Run();
        }
        
    }

    public void SwitchState(IGameState _nextState, string _sceneName = null)
    {
        NextState = _nextState;
        if (! string.IsNullOrEmpty(_sceneName))
        {
            SceneManager.sceneLoaded += SwitchState2; //because scene is loaded before next render and doesnt wait to excecute code
            SceneManager.LoadScene(_sceneName);
        }
        else
        {
            SwitchState2();
        }

    }

    public void SwitchState2(Scene scene = new Scene(), LoadSceneMode mode = LoadSceneMode.Single)
    {
        SceneManager.sceneLoaded -= SwitchState2;

        //clean up
        if (currentState != null)
        {
            currentState.OnStateSwitch -= SwitchState;
            currentState.End();
        }

        NextState.gameManager = gameManager;
        //initialize
        NextState.Start();
        NextState.OnStateSwitch += SwitchState;

        //store current
        currentState = NextState;
    }
}
