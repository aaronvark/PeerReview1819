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
