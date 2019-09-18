using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public IState currentState;
    public MenuData menuData;

    private Dictionary<System.Type, IState> dictionary = new Dictionary<System.Type, IState>();

    //Starts off in the MainMenuState.
    public void Start()
    {
        SwitchState(typeof(MainMenuState));
    }

    //Switches state, needs a new state type to function.
    public void SwitchState(System.Type _newStateType)
    {
        IState _newState = null;

        //Go through dictionary.
        foreach (KeyValuePair<System.Type, IState> pair in dictionary)
        {
            //If found select
            if (pair.Key == _newStateType)
            {
                _newState = pair.Value;
            }
        }

        if (_newState == null)
        {
            _newState = (IState)System.Activator.CreateInstance(_newStateType);
            dictionary.Add(_newStateType, _newState);
        }

        if (currentState != null)
        {
            currentState.OnStateSwitch -= SwitchState;
            currentState.OnExit();
        }

        _newState.OnEnter(menuData);
        _newState.OnStateSwitch += SwitchState;

        currentState = _newState;
    }
}