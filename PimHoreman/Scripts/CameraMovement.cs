﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	[SerializeField] private Vector3 offset;
	[SerializeField] private float smoothTime = 0.5f;
	[SerializeField] private GameObject ball;
	private Vector3 velocity = Vector3.zero;
	private Camera cam;

	private void Awake()
	{
		cam = Camera.main;
	}

	private void Update()
    {
		MoveCamera();
    }

	private void MoveCamera()
	{
		//transform.position = Vector3.SmoothDamp((transform.position), ball.transform.position + offset, ref velocity, smoothTime);
		Vector3 _point = cam.WorldToViewportPoint(ball.transform.position);
		Vector3 _delta = ball.transform.position - cam.ViewportToWorldPoint(offset); //(new Vector3(0.5, 0.5, point.z));
		Vector3 _destination = transform.position + _delta;
		transform.position = Vector3.SmoothDamp(transform.position, _destination, ref velocity, smoothTime);
	}
}