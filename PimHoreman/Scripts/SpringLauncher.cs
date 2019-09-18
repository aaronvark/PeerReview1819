using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringLauncher : MonoBehaviour
{
	[Header("Amount of Velocity/Power:")]
	[SerializeField] private int plungerVelocity;

	[Header("Type of Ball:")]
	[SerializeField] private Ball ball;

	private float pullSpeed = 0.5f;
	private float resetSpeed = 10f;
	private float deltaPos;

	private bool isActive;
	private bool isResetting;

	private Vector3 pullBackAmount;
	private Vector3 startPos;

	private void Start()
	{
		isActive = true;
		startPos = gameObject.transform.position;
	}

	private void Update()
	{
		PlungerActivity();
	}

	private void PlungerActivity()
	{
		if (!isResetting && isActive && Input.GetKey(KeyCode.Space) && Mathf.Abs(gameObject.transform.position.y - startPos.y) < 1f)
		{
			isResetting = false;
			pullBackAmount = new Vector3(0f, -pullSpeed * Time.deltaTime, 0f);
			gameObject.transform.Translate(pullBackAmount);
		}

		if (Input.GetKeyUp(KeyCode.Space))
		{
			deltaPos = startPos.y - gameObject.transform.position.y;
			ball.VelocityBall(deltaPos, plungerVelocity, ForceMode.VelocityChange);
			isResetting = true;
			isActive = false;
		}

		if (isResetting)
		{
			if (gameObject.transform.position.y < startPos.y)
			{
				Vector3 _moveToStartPos = new Vector3(0f, resetSpeed * Time.deltaTime, 0f);
				gameObject.transform.Translate(_moveToStartPos);
			}
			else
			{
				isResetting = false;
			}
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == Tags.Ball)
		{
			isActive = true;	
		}
	}
}