using UnityEngine;

[SharedBetweenAnimators]
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
        var ghostBehaviour = animator.GetComponent<Ghost>().Behaviour;
        animator.SetInteger(integerName, (int)ghostBehaviour);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.SetInteger(integerName, default);
    }
}
