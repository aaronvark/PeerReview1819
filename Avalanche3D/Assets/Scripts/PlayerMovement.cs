using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Scripts
    public GameManager GameManagerInstance { get; private set; }

    //References
    private GameObject Player;
    public GroundedChecker groundedChecker;
    private Animator animator;

    //Components
    private CharacterController characterController;

    //Public variables
    public float Speed;
    public float JumpForce;
    public float WallJumpForce;
    public float GravityModifier;
    public float AgainstWallGravityModifier;
    public float WallJumpMoveCooldown;

    //Private variables
    private Vector3 moveDirection;
    private bool walled;
    private bool canMove;

    private void Start()
    {
        GameManagerInstance = InstanceManager<GameManager>.GetInstance("GameManager");
        Player = GameManagerInstance.Player;
        characterController = Player.GetComponent<CharacterController>();
        animator = Player.GetComponent<Animator>();
        walled = false;
        canMove = true;
    }

    private void Update()
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

    private void Jump()
    {
        if (characterController.isGrounded)
        {
            moveDirection.y = 0f;

            if (Input.GetButtonDown("Jump"))
            {
                animator.SetTrigger("Jump");
                moveDirection.y = JumpForce;
            }

        }
    }

    private void ApplyGravity()
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
