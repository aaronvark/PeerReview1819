using UnityEngine;

public class Ball : MonoBehaviour
{
	[SerializeField] private float speed = 10f;

	private Rigidbody rb;
	private PaddleDirectionMath paddleDirMath;

	private void Start()
    {
		paddleDirMath = GetComponent<PaddleDirectionMath>();
		rb = GetComponent<Rigidbody>();
		rb.velocity = Vector2.up * speed;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == Tags.Paddle){
			float x = paddleDirMath.hitFactor(transform.position, collision.transform.position, collision.collider.bounds.size.x);

			Vector2 dir = new Vector2(x, 1).normalized;
			rb.velocity = dir * speed;
		}
	}
}