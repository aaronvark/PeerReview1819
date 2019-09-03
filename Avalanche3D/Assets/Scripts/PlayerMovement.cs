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
    public float RotationSpeed;
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
        //Get Axises
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        moveDirection = new Vector3(horizontalAxis * Speed, moveDirection.y ,verticalAxis * Speed);
    }

    void Jump()
    {
        if (groundedChecker.Grounded)
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
