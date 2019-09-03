using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    GameManager gameManager;
    GameObject Player;
    public float Speed;

    private void Start()
    {
        gameManager = GameManager.Instance;
        Player = gameManager.Player;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        Player.transform.position = new Vector3(transform.position.x + (horizontalAxis * Speed / 10),transform.position.y, transform.position.z + (verticalAxis * Speed / 10));
    }
}
