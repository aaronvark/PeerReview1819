using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinky : Ghost
{
    [SerializeField]
    private float ambushDistance = 4f;

    private Transform player = null;
    private Movement2D movement = null;

    public override void StateEnter()
    {
        player = Player.Instance.transform;
        movement = transform.GetComponent<Movement2D>();
    }

    public override void StateUpdate()
    {
        Vector2 ambushPosition = (Vector2)player.position + (Vector2)player.right * ambushDistance;
        Vector2 distance = ambushPosition - (Vector2)transform.position;
        Vector2 direction = distance.normalized;

        movement.Move(direction);
    }
}
