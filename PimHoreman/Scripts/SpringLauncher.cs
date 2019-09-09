using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringLauncher : MonoBehaviour
{
	private float pullSpeed = 0.5f;
	private float resetSpeed = 10f;
	private bool isResetting;
	private bool isActive;
	private Vector3 startPos;

	[SerializeField] private GameObject ball;

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
			Vector3 _pullBackAmount = new Vector3(0f, -pullSpeed * Time.deltaTime, 0f);
			gameObject.transform.Translate(_pullBackAmount);
		}

		if (Input.GetKeyUp(KeyCode.Space))
		{
			ball.GetComponent<Rigidbody>().AddForce(Vector3.up * 10, ForceMode.Force);
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
		isActive = true;
	}
}