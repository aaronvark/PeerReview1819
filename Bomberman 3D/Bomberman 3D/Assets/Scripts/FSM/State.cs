using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public FSM fsm;
    public StateEnum id;

    protected IUser _iUser;
    protected ITarget _iTarget;

    public void Init(FSM _owner)
    {
        fsm = _owner;
    }

    public virtual void OnEnter(IUser _iUser, ITarget _iTarget)
    {
        this._iUser = _iUser;
        this._iTarget = _iTarget;
    }

    public abstract void OnUpdate();
    public abstract void OnExit();
}
