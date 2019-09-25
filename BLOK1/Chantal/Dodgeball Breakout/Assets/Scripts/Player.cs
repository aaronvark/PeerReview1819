using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
	[SerializeField]
	private float moveSpeed, lives, minPosClamp, maxPosClamp;
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

	//reduces health if a rock hits the player
	public void Health() 
	{
		lives--;

		if(lives <= 0) 
		{
			manager.GameOver();
		}
	}

	//moves the player based on input and 
	//limits the player to stay within the gamefield
	private void Movement() 
	{
		float _xAxis = Input.GetAxis("Horizontal");
		Vector3 _movement = new Vector3(_xAxis, 0, 0) * moveSpeed * Time.deltaTime;
		Vector3 _newPosition = transform.position + _movement;
		float _newPosX = Mathf.Clamp(_newPosition.x, minPosClamp, maxPosClamp);
		_newPosition.x = _newPosX;
		
		rb.MovePosition(_newPosition);
	}
}
