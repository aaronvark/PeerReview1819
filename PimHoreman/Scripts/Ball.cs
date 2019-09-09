using UnityEngine;

public class Ball : MonoBehaviour, IReset
{
	public Vector3 ResetPosition { get { return startPosition; } set { startPosition = value; } }

	private float ballSpeed;
	private Vector3 startPosition;
	private Rigidbody rb;

	public void ResetObject()
	{
		rb.angularVelocity = Vector3.zero;
		rb.velocity = Vector3.zero;
		transform.position = startPosition;
	}

	public void VelocityBall(float _deltaPosition, int _force)
	{
		rb.AddForce(Vector3.up * (_deltaPosition * _force), ForceMode.Force);
		ballSpeed = _deltaPosition * _force;
	}

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == Tags.Paddle)
		{
			float _x = UtilityMath.HitFactor(transform.position, collision.transform.position, collision.collider.bounds.size.x);

			Vector2 _dir = new Vector2(_x, 1).normalized;
			rb.velocity = _dir * ballSpeed;
		}
	}
}