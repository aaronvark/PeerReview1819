using System.Collections;
using System.Collections.Generic;
using Breakin.GameManagement;
using Breakin.Pooling;
using UnityEngine;

namespace Breakin
{
    public class Spawner : MonoBehaviour
    {
        /// <summary>
        /// Invoked when all rings have been spawned
        /// </summary>
        public event System.Action SpawnerExhausted;

        /// <summary>
        /// Invoked when the spawner wanted to spawn a ring but couldn't because the screen was full (game over condition)
        /// </summary>
        public event System.Action MaxRingsReached;

        [SerializeField] private LevelData data;

        public float Radius => data.SpawnRadius;
        public MultiPrefabPool<Block> BlockPool { get; private set; }

        private List<Ring> rings;
        private int ringsSpawned;
        private Coroutine ringSpawning;

        public void SetLevelData(LevelData data)
        {
            Deactivate();

            this.data = data;
        }

        public void Activate()
        {
            ringsSpawned = 0;
            rings = new List<Ring>();

            if (BlockPool == null) BlockPool = new MultiPrefabPool<Block>(data.BlockCountRing, transform, data.StandardBlock);

            ringSpawning = StartCoroutine(TimedRingSpawning());
        }

        public void Deactivate()
        {
            StopCoroutine(ringSpawning);
            BlockPool.ReclaimAll();
        }

        /// <summary>
        /// Spawns rings every x seconds, as defined by the spawner settings
        /// </summary>
        private IEnumerator TimedRingSpawning()
        {
            while (ringsSpawned < data.RingCount)
            {
                SpawnRing();
                ringsSpawned++;
                yield return new WaitForSeconds(data.TimeBetweenRings);
            }
        }

        private void SpawnRing()
        {
            // Create a new ring and add it to the list for future reference
            Ring _ring = new Ring(rings.Count, this, data.BlockCountRing, data.StandardBlock);

            // Register to remove the ring from the list when it is broken.
            _ring.RingBroken += RemoveRing;

            // Move the current rings outward before adding the new ring to the list
            rings.ForEach(ring => ring.UpdateBlockPositions(data.RingSpacing, Radius, ringsSpawned));

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