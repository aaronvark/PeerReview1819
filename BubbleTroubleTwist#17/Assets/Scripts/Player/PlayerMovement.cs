using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bas.Interfaces;

public class PlayerMovement : AbstractAvatarClass, IStats<PlayerData>
{
    public PlayerData playerInput;

    private void FixedUpdate()
    {
        //if (currentPlayerData != null)
            Movement();
    }

    private void Movement()
    {
        float _vertical = Input.GetAxis(playerInput.verticalAxis);
        float _horizontal = Input.GetAxis(playerInput.horizontalAxis);
        rBody.velocity = new Vector3(_horizontal * speed, rBody.velocity.y, _vertical * speed);
    }

    public void SetStats(PlayerData data)
    {
        playerInput = data;
    }
}
