using UnityEngine;

// TODO: Write individual StateMachineBehaviours added to animator override controllers for each individual ghost
// Calls the individual ghost behaviour through finding their individual ghost scripts and accessing their State functions
public class Standard : StateMachineBehaviour
{
    private Ghost ghost = null;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ghost = animator.GetComponent<Ghost>();

        // Enable all colliders on this gameobject
        Collider2D[] playerColliders = animator.GetComponentsInChildren<Collider2D>();
        for (int i = 0; i < playerColliders.Length; i++)
            playerColliders[i].enabled = true;

        animator.ResetTrigger("SetVulnerable");
        ghost.StateEnter();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ghost.StateUpdate();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ghost.StateExit();
    }
}
