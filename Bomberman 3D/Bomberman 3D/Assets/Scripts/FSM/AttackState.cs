using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public AttackState(StateEnum id)
    {
        this.id = id;
    }

    public override void OnEnter(IUser _iUser, ITarget _iTarget)
    {
        base.OnEnter(_iUser, _iTarget);
    }

    public override void OnExit()
    {
        Debug.Log("Exit Time");
    }

    public override void OnUpdate()
    {
        PlaceBomb();
    }

    private void PlaceBomb()
    {
        if (_iUser.bomb.bombDeployCheck)
        {
            _iUser.bomb.Deployed();
            _iUser.DeployBomb();
            fsm.SwitchState(StateEnum.Hide);
        }
        fsm.SwitchState(StateEnum.Hide);
    }
}
