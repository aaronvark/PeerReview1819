﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinky : Ghost
{
    private Transform player = null;
    private Movement2D movement = null;

    public override void StateEnter()
    {
        player = Player.Instance.transform;
        movement = transform.GetComponent<Movement2D>();
    }

    public override void StateUpdate()
    {
        Vector2 distance = (Vector2)player.position - (Vector2)transform.position;
        Vector2 direction = distance.normalized;
        movement.Move(direction);
    }
}
