using System.Collections.Generic;
using Breakin.Pooling;
using UnityEngine;

namespace Breakin
{
    public class Spawner : MonoBehaviour
    {
        public float Radius => spawnRadius;
        public BlockPool BlockPool { get; private set; }

        [SerializeField] private Block standardBlock;
        [SerializeField] private float spawnRadius;
        [SerializeField] private int blockCountRing;

        private List<Ring> rings;

        private void Start()
        {
            rings = new List<Ring>();
            
            BlockPool = new BlockPool(5, transform, standardBlock);

            SpawnRing();
        }

        /// <summary>
        /// Spawns a new ring of blocks
        /// </summary>
        private void SpawnRing()
        {
            // Create a new ring and add it to the list for future reference
            rings.Add(new Ring(rings.Count, this, blockCountRing, standardBlock));
        }
    }
}