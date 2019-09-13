using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public IdleState(StateEnum id)
    {
        this.id = id;
    }

    public override void OnEnter()
    {
        Debug.Log("I am standing still");
    }

    public override void OnExit()
    {
        Debug.Log("Exit Time");
    }

    public override void OnUpdate()
    {
        fsm.SwitchState(StateEnum.Walk);
    }
}
