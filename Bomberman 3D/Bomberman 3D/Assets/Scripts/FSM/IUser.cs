using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IUser
{
    Transform transform { get; }
    NavMeshAgent navMeshAgent { get; }

    float rayCastLength { get; }
    Vector3 forward { get; }

    Bomb bomb { get; }

    void DeployBomb();
}
