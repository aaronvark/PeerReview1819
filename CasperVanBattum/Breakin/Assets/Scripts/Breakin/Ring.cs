using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Breakin
{
    public delegate void RingEmpty(Ring ring);

    public class Ring
    {
        /// <summary>
        /// Invoked when every block in the ring has been destroyed by the player.
        /// </summary>
        public event RingEmpty RingBroken;

        private readonly List<Block> blocks;
        private readonly int index;

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
        /// Updates the positions of the blocks in the ring, usually when a new ring is added in the game. Each ring is
        /// then moved outward in spawning order (first ring is moved furthest outward). 
        /// </summary>
        /// <param name="spacing">Spacing between rings.</param>
        /// <param name="baseRadius">Radius of the first ring (most inwards).</param>
        /// <param name="currentRingCount">Number of rings currently present in the level.</param>
        public void UpdateBlockPositions(float spacing, float baseRadius, int currentRingCount)
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                ApplyBlockPositionAndRotation(blocks[i], baseRadius + spacing * (currentRingCount - index), i);
            }
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
                
                Block _block = spawner.BlockPool.GetBlock(blockPrefabs[_prefabIndex]);

                
                _block.BlockBroken += CheckRingEmpty;
                
                // Set block properties
                // TODO this is temporary, to be removed
                _block.SetColor(new Color(index / 5f, .5f, .5f));
                
                if (_block.GetType() == typeof(BasicBlock))
                {
                    ((BasicBlock) _block).Value = 1;
                }

                ApplyBlockPositionAndRotation(_block, spawner.Radius, i);
                blocks.Add(_block);
            }
        }

        private void ApplyBlockPositionAndRotation(Block block, float radius, int blockIndex)
        {
            // Calculate the angle between each block
            float _distrAngle = 360f / blocks.Capacity;
            // Calculate the angle this current block is at
            float _pointAngle = _distrAngle * blockIndex;

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

        /// <summary>
        /// Checks if all the blocks are disabled. When all the blocks are disabled the ring should return them all to
        /// the pool and notify the rest of the program that a ring has just been destroyed entirely. Method is meant
        /// to be called each time one of this ring's blocks break.
        /// </summary>
        private void CheckRingEmpty()
        {
            if (blocks.All(b => !b.gameObject.activeSelf))
            {
                ReleaseBlocks();
                RingBroken?.Invoke(this);
            }
        }

        /// <summary>
        /// The blocks in this ring are marked as being in use to the object pooler as long as there is at least one
        /// active block in the ring. This method releases all the blocks in the ring and effectively returns them to
        /// the object pool
        /// </summary>
        private void ReleaseBlocks()
        {
            blocks.ForEach(b => b.IsActive = false);
        }
    }
}