using System;
using System.Collections;
using System.Collections.Generic;
using Breakin.Pooling;
using UnityEngine;

namespace Breakin
{
    public class Spawner : MonoBehaviour
    {
        /// <summary>
        /// Invoked when all rings have been spawned
        /// </summary>
        public event Action SpawnerExhausted;

        public float Radius => spawnRadius;
        public MultiPrefabPool<Block> BlockPool { get; private set; }

        [SerializeField] private Block standardBlock; // Block prefab TODO make this a list; define a list for each individual ring
        [SerializeField] private float spawnRadius = 3; // Radius of the most recent ring
        [SerializeField] private int blockCountRing = 10; // Block count in each ring TODO specify block count for each individual ring
        [SerializeField] private int ringCount = 5; // Number of rings to spawn
        [SerializeField] private float ringSpacing = .3f; // Distance between each ring (center to center)
        [SerializeField] private float timeBetweenRings = 30; // In secconds

        private List<Ring> rings;
        private int ringsSpawned;

        private void Start()
        {
            rings = new List<Ring>();

            BlockPool = new MultiPrefabPool<Block>(blockCountRing, transform, standardBlock);

            StartCoroutine(TimedRingSpawning());
        }

        /// <summary>
        /// Spawns rings every x seconds, as defined by the spawner settings
        /// </summary>
        private IEnumerator TimedRingSpawning()
        {
            while (ringsSpawned < ringCount)
            {
                SpawnRing();
                ringsSpawned++;
                yield return new WaitForSeconds(timeBetweenRings);
            }
        }

        private void SpawnRing()
        {
            // Create a new ring and add it to the list for future reference
            Ring _ring = new Ring(rings.Count, this, blockCountRing, standardBlock);
            
            // Register to remove the ring from the list when it is broken.
            _ring.RingBroken += RemoveRing;
            
            // Move the current rings outward before adding the new ring to the list
            rings.ForEach(ring => ring.UpdateBlockPositions(ringSpacing, Radius, ringsSpawned));
            
            rings.Add(_ring);
        }

        /// <summary>
        /// Remove a ring from the list of currently active/unbroken ring, meant to be invoked by a RingBroken event.
        /// Will also check if this is the last ring the player had to break to finish this spawner/level
        /// </summary>
        /// <param name="ring"></param>
        private void RemoveRing(Ring ring)
        {
            rings.Remove(ring);
            CheckRingsLeft();
        }

        /// <summary>
        /// Invokes the SpawnerExhausted event when all iff rings are broken, to notify listeners this spawner is done
        /// </summary>
        private void CheckRingsLeft()
        {
            if (rings.Count == 0)
            {
                SpawnerExhausted?.Invoke();
            }
        }
    }
}