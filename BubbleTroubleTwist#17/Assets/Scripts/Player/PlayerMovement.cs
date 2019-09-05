using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : AbstractAvatarClass
{
    private void FixedUpdate()
    {
        //if (currentPlayerData != null)
            Movement();
    }

    private void Movement()
    {
        float _vertical = Input.GetAxis(entityStats.verticalAxis);
        float _horizontal = Input.GetAxis(entityStats.horizontalAxis);
        rBody.velocity = new Vector3(_horizontal * speed, rBody.velocity.y, _vertical * speed);
    }
}
