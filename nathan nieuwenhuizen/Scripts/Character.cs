using UnityEngine;

public class Character : MonoBehaviour, IDestroyable
{
    //movement
    [Range(1, 20f)]
    [SerializeField]
    private float walkSpeed = 5f;
    [Range(5, 30f)]
    [SerializeField]
    private float jumpForce = 10f;
    [Range(1, 10)]
    [SerializeField]
    private float gravityScale = 1f;

    //components
    [SerializeField]
    private Gun gun;
    private Rigidbody2D rb;

    //states
    private bool dead = false;
    private bool inAir = true;

    [SerializeField]
    private int health = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionStay2D(Collision2D _collision)
    {
        inAir = false;
    }

    void OnCollisionExit2D(Collision2D _collision)
    {
        rb.gravityScale = gravityScale;
        inAir = true;
    }


    public void Walking(float _h_input)
    {
        Vector3 tempRb = rb.velocity;
        tempRb.x = _h_input * walkSpeed;
        rb.velocity = tempRb;
    }

    public void Jump()
    {
        if (inAir) { return; }

        Vector3 tempRb = rb.velocity;
        tempRb.y = jumpForce;
        rb.velocity = tempRb;
    }

    //to give the player move control in their jump height
    public void CancelJump(bool _onlyWhenUp = true)
    {
        if (rb.velocity.y > 0 || !_onlyWhenUp){
            Vector3 tempRb = rb.velocity;
            tempRb.y = 0;
            rb.velocity = tempRb;
        }
    }

    public void Shoot()
    {
        gun.Shoot();
    }

    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
        }
    }

    public void TakeDamage(int val)
    {
        Health -= val;
        if (Health == 0)
        {
            Death();
        }
    }

    public void Death()
    {
        rb.gravityScale = gravityScale;
        dead = true;
        gameObject.SetActive(false);
    }
}
