using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum StateEnum { Idle, Walk, Attack, Hide }

public class Enemy : Actor, IState
{
    public FSM fsm;
    public State startState;

    [SerializeField] private float raycastLength;
    private Transform transformEnemy;
    private Vector3[] directions;
    private RaycastHit[] hit;
    private NavMeshAgent navMeshAgent;

    //This is from the IState interface
    NavMeshAgent IState.navMeshAgent => navMeshAgent;
    Vector3[] IState.directions => directions;
    RaycastHit[] IState.hit => hit;
    float IState.raycastLength => raycastLength;

    private void Start()
    {
        transformEnemy = transform;
        directions = new Vector3[] { Vector3.left, Vector3.right, Vector3.forward, Vector3.back };
        hit = new RaycastHit[directions.Length];
        navMeshAgent = GetComponent<NavMeshAgent>();

        fsm = new FSM(StateEnum.Idle, new IdleState(StateEnum.Idle), new WalkState(StateEnum.Walk), 
                    new AttackState(StateEnum.Attack), new HideState(StateEnum.Hide));
    }

    private void Update()
    {
        fsm.OnUpdate();
    }
}
