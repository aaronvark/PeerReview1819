using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkState : State
{
    private NavMeshAgent navMeshAgent;

    public WalkState(StateEnum id)
    {
        this.id = id;
    }

    public override void Start(IState _iState)
    {
        base.Start(_iState);
    }

    public override void OnEnter()
    {

    }

    public override void OnExit()
    {
        Debug.Log("Exit Time");
    }

    public override void OnUpdate()
    {
        Debug.Log("Walking");
        //if (Input.GetMouseButtonDown(0))
        //{
        //    fsm.SwitchState(StateEnum.Attack);
        //}
        //Debug.DrawRay(activeState.transform.position, activeState.directions[3], Color.red);
        if (Physics.Raycast(activeState.transform.position, activeState.directions[3], out activeState.hit[3], activeState.raycastLength))
        {
            if (activeState.hit[3].collider.gameObject.tag == "Wall")
            {
                Debug.DrawRay(activeState.transform.position, activeState.directions[3], Color.green);
                fsm.SwitchState(StateEnum.Attack);
            }
        }
    }
}
