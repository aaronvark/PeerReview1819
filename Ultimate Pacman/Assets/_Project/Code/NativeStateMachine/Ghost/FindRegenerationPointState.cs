// Check FindRegenerationPoint script for comments

using System.Collections;
using UnityEngine;

public class FindRegenerationPointState : AbstractState
{
    [SerializeField]
    private float distanceFromEdge = 1f;
    [SerializeField]
    private float distanceFromPoint = 0.1f;

    private Ghost ghost = null;
    private Movement2D movement = null;

    private Vector2 regenerationPoint;
    private float sqrDistanceFromPoint;

    public FindRegenerationPointState(Transform transform) : base(transform) { }

    public override void OnStateEnter()
    {
        ghost = transform.GetComponent<Ghost>();
        movement = transform.GetComponent<Movement2D>();

        Collider2D[] playerColliders = transform.GetComponentsInChildren<Collider2D>();
        for (int i = 0; i < playerColliders.Length; i++)
            playerColliders[i].enabled = false;

        Vector2 currentPosition = transform.position;
        Collider2D border = GameManager.Instance.collider;
        Vector2 closestPoint = border.ClosestPoint(currentPosition);
        Vector2 pointDirection = (closestPoint - currentPosition).normalized;
        regenerationPoint = closestPoint + pointDirection * distanceFromEdge;

        sqrDistanceFromPoint = distanceFromPoint * distanceFromPoint;
    }

    public override void OnStateUpdate()
    {
        Vector2 distance = regenerationPoint - (Vector2)transform.position;

        if (distance.sqrMagnitude <= sqrDistanceFromPoint)
        {
            transform.position = regenerationPoint;
            transform.GetComponent<Animator>().SetTrigger("Regenerate");
            ghost.stateMachine.SwitchState(null);

            return;
        }

        movement.Move(distance);
    }

    public override void OnStateExit()
    {
        ghost.StartCoroutine(SetDefaultState());
    }

    private IEnumerator SetDefaultState()
    {
        Animator animator = transform.GetComponent<Animator>();
        AnimatorClipInfo currentClipInfo = animator.GetCurrentAnimatorClipInfo(0)[0];

        yield return new WaitUntil(() => !animator.GetCurrentAnimatorClipInfo(0)[0].Equals(currentClipInfo));

        float clipLength = 0f;
        clipLength = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;

        yield return new WaitForSeconds(clipLength);

        ghost.stateMachine.SwitchState(ghost.defaultState);
    }
}
