using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball :MonoBehaviour 
{
	[SerializeField]
	private Transform spawn;
	[SerializeField]
	private float moveSpeed, maxLives, strength;
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

	//reduces live when the ball gets out the gamefield
	private void Health() 
	{
		lives--;

		if(lives <= 0) 
		{
			manager.GameOver();
		} 
		else 
		{
			Respawn();
		}
	}

	//break stones
	private void OnCollisionEnter(Collision _collision) 
	{
		IDamageable _takeDmg = _collision.gameObject.GetComponent<IDamageable>();
		if(_takeDmg != null) 
		{
			_takeDmg.TakeDamage(strength);
		}
	}

	//respawn after dying
	private void Respawn() {
		transform.position = spawn.position;
		rb.AddForce(new Vector3(moveSpeed, moveSpeed, 0));
	}

	//if the ball gets out of the game, check for the health
	private void OnTriggerEnter(Collider _other) 
	{
		if(_other.name == "GameField") 
		{
			Health();	
		}
	}
}
