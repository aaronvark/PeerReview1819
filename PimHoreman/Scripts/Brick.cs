using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
	public static Action<int> ScoreUpdateEvent;
	public static Action SpawnItemEvent;
	public DifferentBricks diffBrick;

	[SerializeField] private int lowPoints;
	[SerializeField] private int highPoints;

	private Dictionary<DifferentBricks, Color> bricktionary = new Dictionary<DifferentBricks, Color>();

	private Renderer ren;

	private void Awake()
	{
		ren = GetComponent<Renderer>();
	}

	private void Start()
	{
		bricktionary.Add(DifferentBricks.Green, Color.green);
		bricktionary.Add(DifferentBricks.Orange, Color.yellow);
		bricktionary.Add(DifferentBricks.Red, Color.red);
		bricktionary.Add(DifferentBricks.Silver, Color.grey);
	}

	private void Update()
	{
		BrickStates();
	}

	private void CallEvent(int _amount)
	{
		if(ScoreUpdateEvent != null)
		{
			ScoreUpdateEvent(_amount);
		}
	}

	private void BrickStates()
	{
		switch (diffBrick)
		{
			case DifferentBricks.Red:
				ren.material.color = bricktionary[DifferentBricks.Red];
				break;
			case DifferentBricks.Orange:
				ren.material.color = bricktionary[DifferentBricks.Orange];
				break;
			case DifferentBricks.Green:
				ren.material.color = bricktionary[DifferentBricks.Green];
				break;
			case DifferentBricks.Silver:
				ren.material.color = bricktionary[DifferentBricks.Silver];
				break;
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == Tags.Ball){
			if ((int)diffBrick == 3)
			{
				CallEvent(lowPoints);
				diffBrick = DifferentBricks.Green;
				return;
			}
			if ((int)diffBrick == 2)
			{
				CallEvent(lowPoints);
				diffBrick = DifferentBricks.Orange;
				return;
			}
			if((int)diffBrick == 1)
			{
				CallEvent(lowPoints);
				diffBrick = DifferentBricks.Red;
				return;
			}
			if ((int)diffBrick == 0)
			{
				CallEvent(highPoints);
				DestroyBrick();
			}		
		}
	}

	private void DestroyBrick()
	{
		if(SpawnItemEvent != null)
		{
			SpawnItemEvent();
		}

		Destroy(gameObject);
	}
}