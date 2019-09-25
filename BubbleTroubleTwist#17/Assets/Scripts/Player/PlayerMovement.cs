using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;

public class PlayerMovement : AbstractAvatarClass, IPoolable
{
    private void FixedUpdate()
    {
        if (PlayerInput != null)
            Movement();
    }

    private void Movement()
    {
        float _vertical = Input.GetAxis(PlayerInput.verticalAxis);
        float _horizontal = Input.GetAxis(PlayerInput.horizontalAxis);
        RBody.velocity = new Vector3(_horizontal * Speed, RBody.velocity.y, _vertical * Speed);
    }
}
