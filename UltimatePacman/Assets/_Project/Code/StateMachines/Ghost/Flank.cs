using UnityEngine;

public class Flank: GhostStateMachineBehaviour
{
    private const float distanceInFrontOfPlayer = 2f;
    private const float flankingPointMultiplier = 2f;

    private Transform flankBase;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        flankBase = animator.GetComponent<Ghost>().FlankBase;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Find the point flankingPointMultiplier times farther than from Blinky (Red ghost) to the player (Pacman)
        Vector2 playerPoint = (Vector2)player.position + (Vector2)player.right * distanceInFrontOfPlayer;
        Vector2 blinkyPoint = (Vector2)flankBase.position;
        Vector2 flankingPoint = blinkyPoint + ((playerPoint - blinkyPoint) * flankingPointMultiplier);

        Vector2 distance = flankingPoint - (Vector2)transform.position;

        movement.Move(distance);
    }
}
