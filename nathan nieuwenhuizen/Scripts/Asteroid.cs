using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private GameObject asteroidPrefab;

    [SerializeField]
    private int willBeDevidedBy = 2;

    private int size = 3;
    private Rigidbody2D rb;
    private Vector2 velocity;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));
        rb.velocity = velocity = transform.right * speed;
        Size = size;
    }

    //when hit by a bullet
    public void Split() {
        size--;
        if (size != 0){
            for (int i = 0; i < willBeDevidedBy; i++) {
                Asteroid asteroid = GameObject.Instantiate(asteroidPrefab).GetComponent<Asteroid>();
                asteroid.Size = size;
            }
        }
        Destroy(gameObject);
    }

    //bounces from the walls
    private void OnCollisionEnter2D(Collision2D _collision) {
        Collider2D _collider = _collision.collider;
        Vector2 _contactPoint = _collision.contacts[0].point;
        Vector2 _center = transform.position;
        Vector2 _delta = _contactPoint - _center;

        Vector2 _newVelocity = velocity;
        if (Mathf.Abs(_delta.x) > Mathf.Abs(_delta.y))
        {
            _newVelocity.x *= -1;
        } else
        {
            _newVelocity.y *= -1;
        }
        velocity = _newVelocity;

        rb.velocity = velocity;
    }

    //scales the size according to the value
    public int Size {
        get {
            return size;
        }
        set {
            size = value;
            transform.localScale = new Vector2(size * 2, size * 2);
        }
    }
}
