using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput
{
    public float[] GetInput(int player)
    {
        float[] input = new float[4];
        //buttons set in Project settings
        if (player == 0)
        {
            input[0] = (int)Input.GetAxisRaw("Player1Hor");
            input[1] = (int)Input.GetAxisRaw("Player1Ver");
            input[2] = (int)Input.GetAxisRaw("Player1Rot");
            input[3] = Input.GetButtonDown("Player1Sav") ? 1 : 0;
        }
        else if(player == 1)
        {
            input[0] = Input.GetAxisRaw("Player2Hor");
            input[1] = Input.GetAxisRaw("Player2Ver");
            input[2] = Input.GetAxisRaw("Player2Rot");
            input[3] = Input.GetButtonDown("Player2Sav") ? 1 : 0;
        }
        else
        {
            Debug.LogWarning("Player number unknown");
        }

        return input;
    }
}
