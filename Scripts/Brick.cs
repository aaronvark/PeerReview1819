using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
	[SerializeField] private DifferentBricks diffBrick;

	private void Update()
	{
		BrickStates();
	}

	private void BrickStates()
	{
		switch (diffBrick)
		{
			case DifferentBricks.Red:
				gameObject.GetComponent<Renderer>().material.color = Color.red;
				break;
			case DifferentBricks.Orange:
				gameObject.GetComponent<Renderer>().material.color = Color.yellow;
				break;
			case DifferentBricks.Green:
				gameObject.GetComponent<Renderer>().material.color = Color.green;
				break;
			case DifferentBricks.Silver:
				gameObject.GetComponent<Renderer>().material.color = Color.gray;
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
				Destroy(gameObject);
			}
		}
	}
}
