using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private List<IState> states = new List<IState>();
    private IState currentState;

    public StateMachine(List<IState> _states)
    {
        this.states = _states;
    }

    /// <summary>
    /// Setting new state
    /// </summary>
    public void SwitchState(States _nextState)
    {

        Debug.Log(_nextState);

        //exiting state
        if(currentState != null)
        {
            currentState.Exit();
        }

        //Setting next state
        currentState = states[(int)_nextState];

        //entering next state
        states[(int)_nextState].Enter();

    }
}
public enum States
{
    MainMenu,
    PlayState,
    OptionState,
    ExitState
}