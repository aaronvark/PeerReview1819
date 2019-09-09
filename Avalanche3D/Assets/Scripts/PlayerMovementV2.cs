using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementV2 : MonoBehaviour
{
    //Scripts
    GameManager gameManager;

    //References
    GameObject Player;
    public GroundedChecker groundedChecker;
    Rigidbody rigidbody;

    //Public variables
    public float Speed;
    public float JumpForce;
    public float GravityModifier;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        Player = gameManager.Player;
        rigidbody = Player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        rigidbody.AddForce((transform.forward * vertical + transform.right * horizontal).normalized * Speed);

    }

    void Jump()
    {
        if (Input.GetButton("Jump") && groundedChecker.Grounded)
        {
            rigidbody.AddForce(transform.up * JumpForce);
        }
        if (Input.GetButton("Jump") && groundedChecker.Walled)
        {
            rigidbody.AddForce(transform.up * JumpForce + groundedChecker.Opposite * JumpForce);
        }
    }
}
