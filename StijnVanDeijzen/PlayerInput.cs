using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerInput
{
    public enum InputType
    {
        HORIZONTAL,
        VERTICAL,
        ROTATE,
        SAVE
    }

    public static float Get(int player, InputType type)
    {
        //buttons set in Project settings
        //Axis names are Player + number + InputType, example: Player1Hor
        switch (type)
        {
            case InputType.HORIZONTAL:
                return Input.GetAxisRaw("Player" + player + "Hor");
            case InputType.VERTICAL:
                return Input.GetAxisRaw("Player" + player + "Ver");
            case InputType.ROTATE:
                return Input.GetAxisRaw("Player" + player + "Rot");
            case InputType.SAVE:
                return Input.GetButtonDown("Player" + player + "Sav") ? 1 : 0;
        };
        return 0;
    }

    public static bool GetPause()
    {
        return Input.GetButtonDown("Pause");
    }
}
