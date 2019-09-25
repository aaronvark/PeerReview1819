using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkState : State
{
    private int maxDistanceToPlayer = 10;
    private int maxDistanceToLocation = 5;

    private Vector3 destinationPoint;
    private bool needDestination = true;
    private int distanceToPlayer;
    private int distanceToLocation;

    private RaycastHit hit;

    public WalkState(StateEnum id)
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
        DirectionManager();
        CheckRayCast();
    }

    private void DirectionManager()
    {
        distanceToLocation = Convert.ToInt32((Vector3.Distance(_iUser.navMeshAgent.destination, destinationPoint)));
        distanceToPlayer = Convert.ToInt32((Vector3.Distance(_iUser.navMeshAgent.destination, _iTarget.player.transform.position)));

        if (distanceToPlayer <= maxDistanceToPlayer)
        {
            _iUser.navMeshAgent.destination = _iTarget.player.transform.position;
        }
        else
        {
            _iUser.navMeshAgent.destination = destinationPoint;
        }

        if (_iUser.navMeshAgent.destination == destinationPoint || distanceToLocation <= maxDistanceToLocation)
        {
            needDestination = true;
        }

        if (needDestination)
        {
            SetDestination();
            needDestination = false;
        }
    }

    private void CheckRayCast()
    {
        if (Physics.Raycast(_iUser.transform.position, _iUser.transform.forward, out hit, _iUser.rayCastLength))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                Debug.DrawRay(_iUser.transform.position, _iUser.transform.forward, Color.green);
                fsm.SwitchState(StateEnum.Attack);
            }
            else if (hit.collider.gameObject.tag == "Wall")
            {
                Debug.DrawRay(_iUser.transform.position, _iUser.transform.forward, Color.green);
                fsm.SwitchState(StateEnum.Attack);
            }
        }
    }

    private void SetDestination()
    {
        System.Random random = new System.Random();
        int _xPos = random.Next(-13, 13);
        int _zPos = random.Next(-13, 13);
        destinationPoint = new Vector3(_xPos, 1.15f, _zPos);
    }
}
