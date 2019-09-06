using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	[SerializeField] private Vector3 offset;
	[SerializeField] private float smoothTime = 0.3f;
	[SerializeField] private Ball ball;
	private Vector3 velocity = Vector3.zero;

	private void Update()
    {
		MoveCamera();
    }

	private void MoveCamera()
	{
		transform.position = Vector3.SmoothDamp((transform.position), ball.transform.position + offset, ref velocity, smoothTime);
	}
}