using UnityEngine;

public class PaddleInput : MonoBehaviour
{
	[SerializeField] private float paddleSpeed = 8;
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
		float moveHorizontal = Input.GetAxis(horizontal);

		Vector3 movement = new Vector3(moveHorizontal, 0, 0);
		rb.velocity = movement * paddleSpeed;
	}
}