using UnityEngine;

public class Ambush : StateMachineBehaviour
{
    [SerializeField]
    private float ambushDistance = 4f;

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
        Vector2 ambushPosition = player.position + player.right * ambushDistance;
        Vector2 distance = ambushPosition - (Vector2)transform.position;

        movement.Move(distance);
    }
}
