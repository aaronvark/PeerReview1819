using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball :MonoBehaviour 
{
	[SerializeField]
	private Transform spawn;
	[SerializeField]
	private float moveSpeed, maxLives;
	private float lives;
	private Manager manager;
	private Rigidbody rb;

	void Awake() 
	{
		lives = maxLives;
		manager = GameObject.FindObjectOfType<Manager>();
		rb = gameObject.GetComponent<Rigidbody>();
	}

	private void Start() 
	{
		rb.AddForce(new Vector3(moveSpeed, moveSpeed, 0));
	}

	private void Health() 
	{
		lives--;

		if(lives <= 0) 
		{
			manager.GameOver();
		}
	}

	public void ChangeSpeed(float _newSpeed) 
	{
		//rb.AddForce(new Vector3(_newSpeed, _newSpeed, 0));
	}

	private void OnCollisionEnter(Collision _collision) 
	{
		IDamageable _takeDmg = _collision.gameObject.GetComponent<IDamageable>();
		if(_takeDmg != null) 
		{
			_takeDmg.TakeDamage(1);
			ChangeSpeed(1);
		}
	}

	private void OnTriggerEnter(Collider _other) 
	{
		if(_other.name == "GameField") 
		{
			Health();
		}
	}
}
