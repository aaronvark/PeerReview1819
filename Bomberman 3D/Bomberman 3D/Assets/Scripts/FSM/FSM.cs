using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM
{
    public Dictionary<StateEnum, State> states = new Dictionary<StateEnum, State>();

    private State currentState;

    public FSM(StateEnum startState, params State[] statesList)
    {
        foreach(State state in statesList)
        {
            state.Init(this);
            states.Add(state.id, state);
        }

        SwitchState(startState);
    }

    public void SwitchState(StateEnum newState)
    {
        if(currentState != null)
        {
            currentState.OnExit();
        }

        currentState = states[newState];

        if (currentState != null)
        {
            currentState.OnEnter();
        }
    }

    public void OnUpdate()
    {
        if (currentState != null)
        {
            currentState.OnUpdate();
            Debug.Log(currentState);
        }
    }
}
