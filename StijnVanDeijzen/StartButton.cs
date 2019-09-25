using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public static bool isClicked = false;

    public void SetClicked()
    {
        isClicked = true;
    }
}
