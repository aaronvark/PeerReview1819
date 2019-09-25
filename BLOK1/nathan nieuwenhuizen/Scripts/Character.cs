using UnityEngine;

/// <summary>
/// Handles the movement and the health.
/// </summary>
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

    [SerializeField]
    private GameObject deathParticle;
    private Rigidbody2D rb;

    public Gun gun;

    //states
    private bool dead = false;
    private bool inAir = true;

    [SerializeField]
    private int health = 1;

    void Start()
    {
        PoolManager.instance.CreatePool(deathParticle, 1);
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

    public void Trigger()
    {
        gun.Trigger();
    }

    public void TriggerHold()
    {
        gun.TriggerHold();
    }

    public void Untrigger()
    {
        gun.Untrigger();
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
        CameraShake.OnShake?.Invoke(0.2f);
        PoolManager.instance.ReuseObject(deathParticle, transform.position, Quaternion.identity);

        GameManager.Instance.EndScreen();
        gameObject.SetActive(false);
    }
}
