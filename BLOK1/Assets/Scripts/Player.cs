using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Actor
{
    private Transform _myTransform;

    private void Start()
    {
        _myTransform = transform;
    }


    public void PlayerMovement(Vector2 movement)
    {
        _myTransform.position = movement;
    }

    public override void Death()
    {
        // Setup for later development.
    }
}
