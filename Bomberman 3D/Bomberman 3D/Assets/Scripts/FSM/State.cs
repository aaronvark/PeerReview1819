using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public FSM fsm;
    public StateEnum id;

    protected IState activeState;

    public void Init(FSM owner)
    {
        fsm = owner;
    }

    public virtual void Start(IState _iState)
    {
        activeState = _iState;
    }

    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();
}
