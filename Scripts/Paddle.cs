using UnityEngine;

public class Paddle : MonoBehaviour
{
	private PaddleInput paddleInput;
	
	private void Start()
	{
		paddleInput = GetComponent<PaddleInput>();
	}

	private void Update()
	{
		paddleInput.PaddleMovement();
	}
}