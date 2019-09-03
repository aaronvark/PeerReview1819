using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField]
    private float speed = 5f;

    private Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    //disappears when hitting something
    private void OnCollisionEnter2D(Collision2D _collision){
        if (_collision.gameObject.GetComponent<Asteroid>()) {
            _collision.gameObject.GetComponent<Asteroid>().Split();
        }
        if (!_collision.gameObject.GetComponent<Character>()) {
            Destroy(gameObject);
        } 
    }

}
