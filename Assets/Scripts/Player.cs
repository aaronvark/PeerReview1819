using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Actor
{
    //Provides movement for the player
    public void PlayerMovement(Vector2 movement)
    {
        GetComponent<Rigidbody2D>().AddForce(movement);
    }
}