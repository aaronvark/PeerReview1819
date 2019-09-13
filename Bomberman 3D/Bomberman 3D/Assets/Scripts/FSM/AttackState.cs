using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public AttackState(StateEnum id)
    {
        this.id = id;
    }

    public override void Start(IState _iState)
    {
        base.Start(_iState);
    }

    public override void OnEnter()
    {
        Debug.Log("attackTime");
    }

    public override void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public override void OnUpdate()
    {
        Debug.Log("Atting");
    }
}
