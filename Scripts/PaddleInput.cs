using UnityEngine;

public class PaddleInput : MonoBehaviour
{
	[SerializeField] private float speed = 8;
	private Rigidbody rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	public void PaddleMovement()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");

		Vector3 movement = new Vector3(moveHorizontal, 0, 0);
		rb.velocity = movement * speed;
	}
}