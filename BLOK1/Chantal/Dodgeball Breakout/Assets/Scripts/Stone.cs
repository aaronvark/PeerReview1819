using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour, IDamageable
{
	[SerializeField]
	private float maxHealthPoints, pointValue;
	public float HealthPoints 
	{
		get;
		set;
	}

	private Manager manager;

	private void Awake() 
	{
		HealthPoints = maxHealthPoints;
		manager = GameObject.FindObjectOfType<Manager>();
	}

	//recieve damage when the ball hits the stone
	public void TakeDamage(float _damage) 
	{
		HealthPoints -= _damage;

		if (HealthPoints <= 0) 
		{
			manager.CurrentPoints += pointValue;
			manager.RemoveStone(gameObject);
		}
	}
}
