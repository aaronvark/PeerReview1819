using UnityEngine;

public class Flee : StateMachineBehaviour
{
    [SerializeField]
    private float speedMultiplier = .5f;    // Multiply the speed by this value in OnStateEnter, divide by it in OnStateExit

    private Transform transform = null;
    private Transform player = null;
    private Movement2D movement = null;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        transform = animator.transform;
        player = Player.Instance.transform;
        movement = animator.GetComponent<Movement2D>();

        movement.moveSpeed *= speedMultiplier;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 distance = transform.position - player.position;

        movement.Move(distance);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        movement.moveSpeed /= speedMultiplier;
    }
}
