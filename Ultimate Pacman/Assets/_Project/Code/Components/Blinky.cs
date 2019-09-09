using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinky : Ghost
{
    private void FixedUpdate()
    {
        ChaseTarget();
    }

    private void ChaseTarget()
    {
        if (target == null)
            return;

        Vector2 distance = (Vector2)(target - transform.position);
        Vector2 direction = distance.normalized;
        movement.Move(direction);
    }
}
