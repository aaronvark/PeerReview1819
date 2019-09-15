using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;

public class PlayerMovement : AbstractAvatarClass
{
    private void OnEnable()
    {
    }

    private void FixedUpdate()
    {
        if (PlayerInput != null)
            Movement();
    }

    private void Movement()
    {
        float _vertical = Input.GetAxis(PlayerInput.verticalAxis);
        float _horizontal = Input.GetAxis(PlayerInput.horizontalAxis);
        rBody.velocity = new Vector3(_horizontal * Speed, rBody.velocity.y, _vertical * Speed);
    }
}
