using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, InputManager
{
    public event onKeyPressedUp _onKeyPressedUpEvent;
    public event onKeyPressedDown _onKeyPressedDownEvent;
    public event onKeyPressedLeft _onKeyPressedLeftEvent;
    public event onKeyPressedRight _onKeyPressedRightEvent;

    private GameManager _instance;
    private Transform _playerTransform;

    [SerializeField]
    private Player _player;


    private void Awake()
    {
        _instance = this;

        _playerTransform = _player.transform;

        _onKeyPressedUpEvent = _player.PlayerMovement;
        _onKeyPressedDownEvent = _player.PlayerMovement;
        _onKeyPressedLeftEvent = _player.PlayerMovement;
        _onKeyPressedRightEvent = _player.PlayerMovement;
    }

    void Update()
    {
        if (Input.GetKey("w") && _onKeyPressedUpEvent != null)
        {
            _onKeyPressedUpEvent.Invoke(new Vector2(_playerTransform.localPosition.x, _playerTransform.localPosition.y + 0.25f));
        }
        if (Input.GetKey("s") && _onKeyPressedDownEvent != null)
        {
            _onKeyPressedDownEvent.Invoke(new Vector2(_playerTransform.localPosition.x, _playerTransform.localPosition.y - 0.25f));
        }
        if (Input.GetKey("a") && _onKeyPressedLeftEvent != null)
        {
            _onKeyPressedLeftEvent.Invoke(new Vector2(_playerTransform.localPosition.x - 0.25f, _playerTransform.localPosition.y));
        }
        if (Input.GetKey("d") && _onKeyPressedRightEvent != null)
        {
            _onKeyPressedRightEvent.Invoke(new Vector2(_playerTransform.localPosition.x + 0.25f, _playerTransform.localPosition.y));
        }
    }
}

// Up +25 Y
// Down -25 Y
// Left -25 X
// Right +25 Y