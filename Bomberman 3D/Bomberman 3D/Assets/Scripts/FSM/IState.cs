using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IState
{
    Transform transform { get; }
    RaycastHit[] hit { get; }
    NavMeshAgent navMeshAgent { get; }
    float raycastLength { get; }
    Vector3[] directions { get; }
}
