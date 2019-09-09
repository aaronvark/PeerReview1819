using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
	[SerializeField]
	private float moveSpeed, lives;
	private Manager manager;
	private Rigidbody rb;

	void Start() 
	{
		manager = GameObject.FindObjectOfType<Manager>();
		rb = gameObject.GetComponent<Rigidbody>();
	}

	void Update() 
	{
		Movement();
	}

	public void Health() 
	{
		lives--;

		if(lives <= 0) 
		{
			manager.GameOver();
		}
	}

	private void Movement() 
	{
		float _xAxis = Input.GetAxis("Horizontal");
		Vector3 _movement = new Vector3(_xAxis, 0, 0) * moveSpeed * Time.deltaTime;

		rb.MovePosition(transform.position + _movement);
	}
}
