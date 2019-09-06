using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
	[SerializeField] private DifferentBricks diffBrick;

	private Dictionary<DifferentBricks, Color> differentColoredBricks;
	private Dictionary<DifferentBricks, DifferentBricks> changeBricks;
	private Renderer ren;

	private void Awake()
	{
		ren = GetComponent<Renderer>();
	}

	private void Update()
	{
		BrickStates();
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
				diffBrick = DifferentBricks.Green;
				return;
			}
			if (diffBrick == DifferentBricks.Green)
			{
				diffBrick = DifferentBricks.Orange;
				return;
			}
			if(diffBrick == DifferentBricks.Orange)
			{
				diffBrick = DifferentBricks.Red;
				return;
			}
			if (diffBrick == DifferentBricks.Red)
			{
				DestroyBrick();
			}
			
		}
	}

	private void DestroyBrick()
	{
		Destroy(gameObject);
	}
}
