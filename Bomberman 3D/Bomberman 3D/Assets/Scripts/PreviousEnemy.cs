using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PreviousEnemy : Actor
{
    [SerializeField] private float raycastLength;
    [SerializeField] private bool bombIsDeployed;
    [SerializeField] private bool waiting = false;

    private Vector3 bombLocation;

    private NavMeshAgent navMeshAgent;
    public  Player player;
    private RaycastHit[] hit;
    private Vector3[] directions;
    [SerializeField] private AIState currentState;

    public enum AIState
    {
        Idle,
        Walking,
        Running,
        DeployingBomb,
        Die
    }

    private void Start()
    {
        directions = new Vector3[] { Vector3.left, Vector3.right, Vector3.forward, Vector3.back };
        hit = new RaycastHit[directions.Length];
        navMeshAgent = GetComponent<NavMeshAgent>();

    }

    private void Update()
    {
        //SwitchState();
        //bombIsDeployed = bomb.bombDeployCheck;
        navMeshAgent.destination = player.transform.position;
    }
    
    private void SwitchState()
    {
        switch (currentState)
        {
            default:
            case AIState.Idle:
                Idle();
                break;
            case AIState.Walking:
                Walking();
                break;
            case AIState.Running:
                Running();
                break;
            case AIState.DeployingBomb:
                Deploying();
                break;
            case AIState.Die:
                Dying();
                break;
        }
    }

    private void Idle()
    {
        currentState = AIState.Walking;
    }

    private void Walking()
    {
        Debug.DrawRay(transform.position, directions[3], Color.red);
        if (Physics.Raycast(transform.position, directions[3], out hit[3], raycastLength))
        {
            if (hit[3].collider.gameObject.tag == "Wall")
            {
                Debug.DrawRay(transform.position, directions[3], Color.green);
                if (bomb.bombDeployCheck)
                {
                    currentState = AIState.DeployingBomb;
                }
            }
        }
        else
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 5f);
            Debug.Log("Walking");
        }
    }

    private void Running()
    {
        if(!waiting)
        {
            if (Physics.Raycast(transform.position, directions[2], out hit[2], raycastLength))
            {
                transform.Translate(Vector3.left * Time.deltaTime * 5f);
                if (Physics.Raycast(transform.position, directions[0], out hit[0], raycastLength))
                {
                    waiting = true;
                    Debug.DrawRay(transform.position, directions[2], Color.yellow);
                }
            }
            else
            {
                transform.Translate(Vector3.back * Time.deltaTime * 5f);
                Debug.DrawRay(transform.position, directions[2], Color.white);
            }
        }
        else
        {
            transform.Translate(Vector3.left * Time.deltaTime * 0);
            if (bomb.bombDeployCheck)
            {
                Waiting();
            }
        }
    }

    private void Waiting()
    {
        transform.position = Vector3.MoveTowards(transform.position, bombLocation, 5f);
    }

    private void Deploying()
    {
        if (bomb.bombDeployCheck)
        {
            bomb.Deployed();
            DeployBomb();
            bombLocation = transform.position;
            Debug.Log("Bomb is deployed");
        }
        else
        {
            currentState = AIState.Running;
        }

    }

    private void Dying()
    {

    }
}
