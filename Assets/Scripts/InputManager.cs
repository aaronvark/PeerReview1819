using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public delegate void onKeyPressedUp(Vector2 movement);
public delegate void onKeyPressedDown(Vector2 movement);
public delegate void onKeyPressedLeft(Vector2 movement);
public delegate void onKeyPressedRight(Vector2 movement);

public interface InputManager
{
    event onKeyPressedUp onKeyPressedUpEvent;
    event onKeyPressedDown onKeyPressedDownEvent;
    event onKeyPressedLeft onKeyPressedLeftEvent;
    event onKeyPressedRight onKeyPressedRightEvent;
}