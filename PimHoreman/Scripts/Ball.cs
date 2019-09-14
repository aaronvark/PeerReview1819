using UnityEngine;

public class Ball : MonoBehaviour, IReset
{
	public Vector3 ResetPosition { get { return startPosition; } set { startPosition = value; } }

	[SerializeField] private TrailRenderer trailRen;

	private float ballSpeed;
	private int health;
	private int maxHealth = 3;
	private Vector3 startPosition;
	private Rigidbody rb;
	private bool isGameOver;

	public void ResetObject()
	{
		if(isGameOver)
		{
			Debug.Log("Game Over");
			return;
		}
		else
		{
			rb.angularVelocity = Vector3.zero;
			rb.velocity = Vector3.zero;
			transform.position = startPosition;
			health--;
			trailRen.Clear();
		}
	}

	public void VelocityBall(float _deltaPosition, int _force, ForceMode _forceMode)
	{
		rb.AddForce(Vector3.up * (_deltaPosition * _force), _forceMode);
		ballSpeed = _deltaPosition * _force;
	}

	public void ExplosiveForceBall(int _force, Vector3 _position, int _radius)
	{
		rb.AddExplosionForce(_force, _position, _radius);
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
		Debug.Log(health);
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