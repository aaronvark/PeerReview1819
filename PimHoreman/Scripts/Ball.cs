using UnityEngine;
using System;

public class Ball : MonoBehaviour, IReset
{
	public Vector3 ResetPosition { get; set; }
	public int Health { get { return health; } }

	[SerializeField] private TrailRenderer trailRen;

	private Rigidbody rb;
	private float ballSpeed;
	private bool isGameOver;
	private int health;
	private int maxHealth = 3;

	public void ResetObject()
	{
		if(isGameOver)
		{
			Debug.Log("Game Over");
			Destroy(gameObject);
			return;
		}
		else
		{
			rb.angularVelocity = Vector3.zero;
			rb.velocity = Vector3.zero;
			transform.position = ResetPosition;
			health--;
			trailRen.Clear();
		}
	}

	public void VelocityBall(float _deltaPosition, int _force, ForceMode _forceMode)
	{
		rb.AddForce(Vector3.up * (_deltaPosition * _force), _forceMode);
		ballSpeed = _deltaPosition * _force;
	}

	public void ApplyForce(Vector3 _forcePosition, int _power, ForceMode _forceMode)
	{
		Vector3 _direction = _forcePosition + transform.position;
		rb.AddForceAtPosition(_direction * _power, transform.position, _forceMode);
	}

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void Start()
	{
		health = maxHealth;
	}

	private void Update()
	{
		if (health <= 0)
		{
			isGameOver = true;
		}
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