using UnityEngine;

public class Ambush : GhostStateMachineBehaviour
{
    [SerializeField]
    private float ambushDistance = 2f;

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 ambushPosition = player.position + player.right * ambushDistance;
        Vector2 distance = ambushPosition - (Vector2)transform.position;

        movement.Move(distance);
    }
}
