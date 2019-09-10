using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void IFSMDelegate(IFSM _state);

public interface IFSM
{
    //property
    IFSMDelegate OnStateSwitch { get; set; }

    void Enter();
    void Run();
    void Exit();
}

public class BasFSM
{
    private IFSM currentState;

    public BasFSM(IFSM _startState)
    {
        SwitchState(_startState);
    }

    public void Update()
    {
        if(currentState != null)
        {
            currentState.Run();
        }
    }

    private void SwitchState(IFSM _newState)
    {
        //opruimen
        if(currentState != null)
        {
            currentState.OnStateSwitch -= SwitchState;
            currentState.Exit();
        }

        //initialize
        _newState.Enter();
        _newState.OnStateSwitch += SwitchState;

        //opslaan
        currentState = _newState;
    }

}

public class SomeState : IFSM
{
    public IFSMDelegate OnStateSwitch { get; set; }

    public void Enter()
    {
        Debug.Log("Do something");
    }

    public void Run()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        { 
            //OnStateSwitch(...)
        }
    }

    public void Exit()
    {

    }
}


public class DoneState : IFSM
{
    public IFSMDelegate OnStateSwitch { get; set; }

    private float timer = 0;

    public void Enter()
    {
        Debug.Log("Do something");
    }

    public void Run()
    {
        timer += Time.deltaTime;
        if(timer > 10)
        {
            OnStateSwitch(new SomeState());
        }
    }

    public void Exit()
    {

    }
}