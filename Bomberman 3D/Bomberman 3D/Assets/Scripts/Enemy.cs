using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum StateEnum { Walk, Attack, Hide }

public class Enemy : Actor, IUser, ITarget
{
    public FSM fsm;
    public State startState;

    [SerializeField] private float raycastLength;
    private Vector3 forward;
    private NavMeshAgent navMeshAgent;

    //This is from the ITarget interface
    Transform ITarget.player => Player.Instance.transform;
    
    //This is from the IState interface
    NavMeshAgent IUser.navMeshAgent => navMeshAgent;
    
    float IUser.rayCastLength => raycastLength;
    Vector3 IUser.forward => forward;

    Bomb IUser.bomb => bomb;

    private void Awake()
    {
        forward = Vector3.forward;
        navMeshAgent = GetComponent<NavMeshAgent>();

        fsm = new FSM(this, this, StateEnum.Walk, new WalkState(StateEnum.Walk), 
                    new AttackState(StateEnum.Attack), new HideState(StateEnum.Hide));
    }

    private void Update()
    {
        if (fsm != null)
        {
            fsm.OnUpdate();
        }
    }

    public override void Die()
    {
        base.Die();
        ActorManager.Instance.RemoveFromList(this);
        Destroy(this.gameObject);
    }
}
