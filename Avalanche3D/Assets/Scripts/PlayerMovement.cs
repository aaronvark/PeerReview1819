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
    public float WallJumpForce;
    public float GravityModifier;
    public float AgainstWallGravityModifier;
    public float WallJumpMoveCooldown;

    //Private variables
    Vector3 moveDirection;
    bool walled;
    bool canMove;

    private void Start()
    {
        gameManager = GameManager.Instance;
        Player = gameManager.Player;
        characterController = Player.GetComponent<CharacterController>();
        walled = false;
        canMove = true;
    }

    void Update()
    {
        if(canMove)
        {
            Move();
            Jump();
        }
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
        if(walled)
        {
            moveDirection.y = moveDirection.y + Physics.gravity.y * GravityModifier * Time.deltaTime * AgainstWallGravityModifier;

        }
        else
        {
            moveDirection.y = moveDirection.y + Physics.gravity.y * GravityModifier * Time.deltaTime;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(!characterController.isGrounded && hit.normal.y < 0.1f)
        {
            walled = true;
            Debug.DrawRay(hit.point, hit.normal, Color.red, 1.25f);
            if(Input.GetButtonDown("Jump"))
            {
                StartCoroutine(MoveCooldown());
                Debug.DrawRay(hit.point, hit.normal, Color.blue, 1.25f);
                moveDirection = new Vector3(hit.normal.x * WallJumpForce, JumpForce, hit.normal.z * WallJumpForce);
                //moveDirection.y = JumpForce;

            }
        }
        walled = false;
    }

    private IEnumerator MoveCooldown()
    {
        canMove = false;
        yield return new WaitForSeconds(WallJumpMoveCooldown);
        canMove = true;
    }

}
