using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
	[SerializeField] private Brick brick;
	[SerializeField] private Vector2 cellSize = new Vector2(1,1); 

	private int[] level = new int[]
	{
			4,4,4,4,4,4,
			4,3,3,3,3,4,
			4,3,2,2,3,4,
			1,1,1,1,1,1,
	};

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
				Vector3 _spawnPosition = new Vector3(x * cellSize.x, y * -cellSize.y, 0) + transform.position;

				Brick b = Instantiate(brick, _spawnPosition, Quaternion.identity);
				b.transform.parent = transform;

				b.diffBrick = (DifferentBricks)(level[index] -1);
			}	
		}	

		/*
		// terugschrijven naar array
		for (int x = 0; x < 6; ++x)
		{
			for (int y = 0; y < 4; ++y)
			{
				int index = x * 6 + y;
				level[index] = 1;
			}
		}	
		*/
	}
}