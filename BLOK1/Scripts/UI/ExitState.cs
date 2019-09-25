using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitState : IState
{
    public ExitState()
    {

    }

    public event SwitchState switchState;


    public void Enter()
    {
        Exit();
    }


    public void Exit()
    {
        Application.Quit();
    }
}
