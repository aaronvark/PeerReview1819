using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VulnerableBehaviour : GhostBehaviour
{
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 distance = transform.position - player.position;
        Vector2 direction = distance.normalized;

        movement.Move(direction);
    }
}
