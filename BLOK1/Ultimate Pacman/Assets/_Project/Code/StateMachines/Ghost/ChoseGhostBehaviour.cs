using System;
using UnityEngine;

public class ChoseGhostBehaviour : StateMachineBehaviour
{
    private const string integerName = "GhostNr";

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Enable all colliders on this gameobject
        Collider2D[] playerColliders = animator.GetComponentsInChildren<Collider2D>();
        for (int i = 0; i < playerColliders.Length; i++)
            playerColliders[i].enabled = true;

        animator.ResetTrigger("SetVulnerable");

        // Figure out the ghost type and enable the corresponding behaviour
        Type ghostType = animator.GetComponent<Ghost>().GetType();
        int ghostNr = animator.GetInteger(integerName);
        switch (ghostType)
        {
            case Type t when t == typeof(Blinky):
                ghostNr = 1;
                break;
            case Type t when t == typeof(Pinky):
                ghostNr = 2;
                break;
            case Type t when t == typeof(Inky):
                ghostNr = 3;
                break;
            case Type t when t == typeof(Clyde):
                ghostNr = 4;
                break;
            default:
                break;
        }
        animator.SetInteger(integerName, ghostNr);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.SetInteger(integerName, default);
    }
}
