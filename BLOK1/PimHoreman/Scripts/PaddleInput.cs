using UnityEngine;

/// <summary>
/// PaddleInput class, the paddle only moves horizontal when the key is pressed.
/// </summary>
public class PaddleInput : MonoBehaviour
{
	[SerializeField] private float paddleSpeed = 30;
	private Rigidbody rb;
	private string horizontal = "Horizontal";

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		PaddleMovement();
	}

	private void PaddleMovement()
	{
		float _moveHorizontal = Input.GetAxis(horizontal);

		Vector3 _movement = new Vector3(_moveHorizontal, 0, 0);
		rb.velocity = _movement * paddleSpeed;
	}
}