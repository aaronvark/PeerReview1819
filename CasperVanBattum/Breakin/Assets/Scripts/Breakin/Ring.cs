using System.Collections.Generic;
using UnityEngine;

namespace Breakin
{
	public class Ring
	{
		private List<Block> blocks;
		private int index;

		/// <summary>
		/// Creates a new Ring object and spawn its blocks
		/// </summary>
		/// <param name="index">The index of this ring</param>
		/// <param name="spawner"></param>
		/// <param name="blockCount">The number of blocks this ring contains</param>
		/// <param name="blockPrefabs">List of prefabs that will spawn on this ring</param>
		public Ring(int index, Spawner spawner, int blockCount, params Block[] blockPrefabs)
		{
			this.index = index;
			blocks = new List<Block>(blockCount);

			CreateBlocks(spawner, blockCount, blockPrefabs);
		}

		/// <summary>
		/// Instantiates Block objects from a list of Block prefabs in the shape of a ring
		/// </summary>
		/// <param name="spawner">The spawner root</param>
		/// <param name="blockCount">The number of blocks to spawn</param>
		/// <param name="blockPrefabs">The list of block prefabs to randomly choose from</param>
		private void CreateBlocks(Spawner spawner, int blockCount, Block[] blockPrefabs)
		{
			for (int i = 0; i < blockCount; i++)
			{
				// Generate a random index for the prefab
				int _prefabIndex = Random.Range(0, blockPrefabs.Length);

				Block _block = GameObject.Instantiate(blockPrefabs[_prefabIndex], spawner.transform);
				
				if (_block.GetType() == typeof(BasicBlock))
				{
					((BasicBlock) _block).Value = 1;
				}
 
				ApplyBlockPositionAndRotation(_block, spawner.Radius, i);
				blocks.Add(_block);
			}
		}

		private void ApplyBlockPositionAndRotation(Block block, float radius, int index)
		{
			// Calculate the angle between each block
			float _distrAngle = 360f / blocks.Capacity;
			// Calculate the angle this current block is at
			float _pointAngle = _distrAngle * index;

			// Convert angle to radians for use in (co)sine functions
			float _pointAngleRad = _pointAngle * Mathf.Deg2Rad;
			// Calculate this block's position
			Vector3 _point = new Vector3(Mathf.Cos(_pointAngleRad), Mathf.Sin(_pointAngleRad)) * radius;

			// Rotate the block 90 degrees to be perpendicular to the line to the center
			float _blockAngle = _pointAngle + 90;

			// Apply values
			block.transform.localPosition = _point;
			block.transform.localRotation = Quaternion.Euler(0, 0, _blockAngle);
		}
	}
}