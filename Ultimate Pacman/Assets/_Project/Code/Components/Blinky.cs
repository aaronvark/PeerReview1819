using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinky : Ghost
{
    private void FixedUpdate()
    {
        Vector2 distance = target.position - transform.position;
        Vector2 direction = distance.normalized;
        movement.Move(direction);
    }
}
