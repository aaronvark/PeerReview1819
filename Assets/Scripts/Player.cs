using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Actor {

    private void Start() {
        InputManager.instance._keyUpEvent.AddListener(PlayerMovementUp);
        InputManager.instance._keyDownEvent.AddListener(PlayerMovementDown);
        InputManager.instance._keyRightEvent.AddListener(PlayerMovementRight);
        InputManager.instance._keyLeftEvent.AddListener(PlayerMovementLeft);
    }
    
    public void PlayerMovementUp() {
        gameObject.transform.position = new Vector2(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y + 0.25f);
    }

    public void PlayerMovementDown() {
        gameObject.transform.position = new Vector2(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y - 0.25f);
    }

    public void PlayerMovementLeft() {
        gameObject.transform.position = new Vector2(gameObject.transform.localPosition.x - 0.25f, gameObject.transform.localPosition.y);
    }

    public void PlayerMovementRight() {
        gameObject.transform.position = new Vector2(gameObject.transform.localPosition.x + 0.25f, gameObject.transform.localPosition.y);
    }

    public override void Death() {
        // Setup for later development.
    }
}
