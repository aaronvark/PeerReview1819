using UnityEngine;

public class Flee : GhostStateMachineBehaviour
{
    [SerializeField]
    private float speedMultiplier = .5f;    // Multiply the speed by this value in OnStateEnter, divide by it in OnStateExit

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        movement.moveSpeed *= speedMultiplier;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 distance = transform.position - player.position;

        movement.Move(distance);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        movement.moveSpeed /= speedMultiplier;
    }
}
