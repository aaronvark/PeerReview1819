using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The BrickSpawner handles the spawning of the bricks in different rows.
/// </summary>
public class BrickSpawner : MonoBehaviour
{
	[Tooltip("Type of Brick")]
	[SerializeField] private Brick brick;

	[Tooltip("How far apart the bricks are in X and Y Position:")]
	[SerializeField] private Vector2 cellSize = new Vector2(1,1);

	private int[] level = new int[]
	{
		3,3,3,3,4,4,4,3,3,3,3,
		3,2,2,2,4,1,4,2,2,2,3,
		2,2,2,2,2,2,2,2,2,2,2,
	};	

	private void Awake()
	{
		// inlezen & spawnen
		for (int index = 0; index < level.Length; ++index)
		{
			//convert to x & y...
			int x = index % 11;
			int y = index / 11;

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
		for (int x = 0; x < 7; ++x)
		{
			for (int y = 0; y < 6; ++y)
			{
				int index = x * 7 + y;
				level[index] = 1;
			}
		}	
		*/
	}
}