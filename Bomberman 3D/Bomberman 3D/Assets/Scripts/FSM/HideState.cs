using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideState : State
{
    private float maxHideTime = 5f;
    private float moveSpeed = 3.5f;
    private float hideTimer;

    public HideState(StateEnum id)
    {
        this.id = id;
    }

    public override void OnEnter(IUser _iUser, ITarget _iTarget)
    {
        base.OnEnter(_iUser, _iTarget);
        _iUser.navMeshAgent.isStopped = true;
        hideTimer = maxHideTime;
    }

    public override void OnExit()
    {
        Debug.Log("Exit Time");
        _iUser.navMeshAgent.isStopped = false;
    }

    public override void OnUpdate()
    {
        Hiding();
    }

    private void Hiding()
    {
        hideTimer -= Time.deltaTime;

        _iUser.transform.Translate(Vector3.back * Time.deltaTime * moveSpeed);

        if (hideTimer <= 0)
        {
            fsm.SwitchState(StateEnum.Walk);
        }
    }
}
