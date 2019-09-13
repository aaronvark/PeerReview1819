using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
	public static Action<int> ScoreUpdateEvent;
	public static Action SpawnItemEvent;
	public DifferentBricks diffBrick;

	//private Dictionary<DifferentBricks, Color> brickColor = new Dictionary<DifferentBricks, Color>();

	private Renderer ren;


	private void Awake()
	{
		ren = GetComponent<Renderer>();
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
				ren.material.color = Color.red;
				break;
			case DifferentBricks.Orange:
				ren.material.color = Color.yellow;
				break;
			case DifferentBricks.Green:
				ren.material.color = Color.green;
				break;
			case DifferentBricks.Silver:
				ren.material.color = Color.gray;
				break;
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == Tags.Ball){
			if (diffBrick == DifferentBricks.Silver)
			{
				CallEvent(100);
				diffBrick = DifferentBricks.Green;
				return;
			}
			if (diffBrick == DifferentBricks.Green)
			{
				CallEvent(100);
				diffBrick = DifferentBricks.Orange;
				return;
			}
			if(diffBrick == DifferentBricks.Orange)
			{
				CallEvent(100);
				diffBrick = DifferentBricks.Red;
				return;
			}
			if (diffBrick == DifferentBricks.Red)
			{
				CallEvent(500);
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
