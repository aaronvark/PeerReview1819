using UnityEngine;

/// <summary>
/// Ball class, the ball uses a rigidbody to move around and uses 1 Interface (IReset).
/// </summary>
public class Ball : MonoBehaviour, IReset
{
	public Vector3 ResetPosition { get; set; }
	public int Health { get { return health; } }

	[SerializeField] private TrailRenderer trailRen;

	private Rigidbody rb;
	private float ballSpeed;
	private bool isGameOver;
	private int maxHealth = 3;
	private int health;

	/// <summary>
	/// IReset(Interface) Resets the object in ResetZone.cs whenever the ball gets out of bounds.
	/// </summary>
	public void ResetObject()
	{
		if(isGameOver)
		{
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

	/// <summary>
	/// VecolictyBall is to add force to the ball when launced by the Plunger (SpringLauncher.cs)
	/// </summary>
	/// <param name="_deltaPosition"></param>
	/// <param name="_force"></param>
	/// <param name="_forceMode"></param>
	public void VelocityBall(float _deltaPosition, int _force, ForceMode _forceMode)
	{
		rb.AddForce(Vector3.up * (_deltaPosition * _force), _forceMode);
		ballSpeed = _deltaPosition * _force;
	}

	/// <summary>
	/// ApplyForce is to 'apply' force to the ball whenever it hits a object that uses this function. 
	/// </summary>
	/// <param name="_forcePosition"></param>
	/// <param name="_power"></param>
	/// <param name="_forceMode"></param>
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