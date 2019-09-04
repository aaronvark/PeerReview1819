using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Scripts
    GameManager gameManager;

    //References
    GameObject Player;
    public GroundedChecker groundedChecker;

    //Components
    CharacterController characterController;

    //Public variables
    public float Speed;
    public float JumpForce;
    public float GravityModifier;

    Vector3 moveDirection;

    private void Start()
    {
        gameManager = GameManager.Instance;
        Player = gameManager.Player;
        characterController = Player.GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
        Jump();
        ApplyGravity();

        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void Move()
    {
        float yStore = moveDirection.y;

        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        moveDirection = ((transform.forward * verticalAxis) + (transform.right * horizontalAxis)).normalized * Speed;

        moveDirection.y = yStore;
    }

    void Jump()
    {
        if (characterController.isGrounded)
        {
            moveDirection.y = 0f;

            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = JumpForce;
            }
        }
    }

    void ApplyGravity()
    {
        moveDirection.y = moveDirection.y + Physics.gravity.y * GravityModifier * Time.deltaTime;
    }

}
