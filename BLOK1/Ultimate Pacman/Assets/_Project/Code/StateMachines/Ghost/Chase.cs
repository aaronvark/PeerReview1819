using UnityEngine;

public class Chase : StateMachineBehaviour
{
    private Transform transform = null;
    private Transform player = null;
    private Movement2D movement = null;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        transform = animator.transform;
        player = Player.Instance.transform;
        movement = animator.GetComponent<Movement2D>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 distance = player.position - transform.position;

        movement.Move(distance);
    }
}
