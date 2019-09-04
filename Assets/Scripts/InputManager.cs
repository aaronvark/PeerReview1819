using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour {

    public UnityEvent _keyUpEvent = new UnityEvent();
    public UnityEvent _keyDownEvent = new UnityEvent();
    public UnityEvent _keyRightEvent = new UnityEvent();
    public UnityEvent _keyLeftEvent = new UnityEvent();

    public static InputManager instance { get; private set; }

    void Awake() {
        if (instance == null) {

            instance = this;

            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void Update() {

        if (Input.GetKey("w") && _keyUpEvent != null) {
            _keyUpEvent.Invoke();
        }

        if (Input.GetKey("a") && _keyLeftEvent != null) {
            _keyLeftEvent.Invoke();
        }

        if (Input.GetKey("s") && _keyDownEvent != null) {
            _keyDownEvent.Invoke();
        }

        if (Input.GetKey("d") && _keyRightEvent != null) {
            _keyRightEvent.Invoke();
        }
    }
}
