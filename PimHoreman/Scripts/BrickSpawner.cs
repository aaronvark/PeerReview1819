using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
	public int[] level = new int[]
	{
			0,1,1,1,1,0,
			0,1,0,0,1,0,
	};

	public class Block
	{
		public Block(int x, int y, DifferentBricks type) { }
	}

	private void Awake()
	{
		// inlezen & spawnen
		for (int index = 0; index < level.Length; ++index)
		{
			//convert to x & y...
			int x = index % 6;
			int y = index / 6;

			if (level[index] != 0)
			{
				Block b = new Block(x, y, (DifferentBricks)level[index]);
				//Instantiate(b, transform.position, Quaternion.identity);
				Debug.Log(b);
			}	
		}	

		/*
		// terugschrijven naar array
		for (int x = 0; x < 6; ++x)
		{
			for (int y = 0; y < 2; ++y)
			{
				int index = x * 6 + y;
				level[index] = 1;
			}
		}	
		*/
	}
}