using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	[SerializeField] private Vector3 offset;
	[SerializeField] private float smoothTime = 0.3f;
	private Ball ball;
	private Vector3 velocity = Vector3.zero;

	private void Start()
	{
		ball = FindObjectOfType<Ball>();
	}

	private void Update()
    {
		//Vector3 _targetPosition = ball.transform.TransformPoint(new Vector3(0,0,-20	));

		transform.position = Vector3.SmoothDamp((transform.position), ball.transform.position + offset, ref velocity, smoothTime);
		//transform.position = new Vector3(transform.position.x, ball.transform.position.y, transform.position.z);
    }
}