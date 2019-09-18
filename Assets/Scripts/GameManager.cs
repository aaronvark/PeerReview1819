using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, InputManager
{
    public event onKeyPressedUp onKeyPressedUpEvent;
    public event onKeyPressedDown onKeyPressedDownEvent;
    public event onKeyPressedLeft onKeyPressedLeftEvent;
    public event onKeyPressedRight onKeyPressedRightEvent;

    private GameManager instance;
    private Transform playerTransform;

    [SerializeField]
    private Player player;

    //Sets up the instance and events for the void Update
    private void Awake()
    {
        instance = this;

        playerTransform = player.transform;

        onKeyPressedUpEvent = player.PlayerMovement;
        onKeyPressedDownEvent = player.PlayerMovement;
        onKeyPressedLeftEvent = player.PlayerMovement;
        onKeyPressedRightEvent = player.PlayerMovement;
    }

    //Checks for pressed keys and invokes events.
    private void Update()
    {
        if (Input.GetKey("w") && onKeyPressedUpEvent != null)
        {
            onKeyPressedUpEvent.Invoke(new Vector2(0, 0.1f)*Time.deltaTime);
        }
        if (Input.GetKey("s") && onKeyPressedDownEvent != null)
        {
            onKeyPressedDownEvent.Invoke(new Vector2(0, - 0.1f)*Time.deltaTime);
        }
        if (Input.GetKey("a") && onKeyPressedLeftEvent != null)
        {
            onKeyPressedLeftEvent.Invoke(new Vector2(-0.1f, 0)*Time.deltaTime);
        }
        if (Input.GetKey("d") && onKeyPressedRightEvent != null)
        {
            onKeyPressedRightEvent.Invoke(new Vector2(0.1f, 0)*Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.visible = true;
            SceneManager.LoadScene("MenuScene");
        }
    }
}