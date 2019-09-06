using UnityEngine;

public class Ball : MonoBehaviour
{
	[SerializeField] private float speed = 10f;

	private Rigidbody rb;

	private void Awake()
    {
		rb = GetComponent<Rigidbody>();
		//rb.velocity = Vector2.up * speed;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == Tags.Paddle)
		{
			float _x = UtilityMath.HitFactor(transform.position, collision.transform.position, collision.collider.bounds.size.x);

			Vector2 _dir = new Vector2(_x, 1).normalized;
			rb.velocity = _dir * speed;
		}
	}
}