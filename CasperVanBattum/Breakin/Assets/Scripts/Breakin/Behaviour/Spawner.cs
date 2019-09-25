using System.Collections.Generic;
using Breakin.GameManagement;
using Breakin.Pooling;
using UnityEngine;

namespace Breakin.Behaviour
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private LevelData data;

        public float Radius => data.SpawnRadius;
        public MultiPrefabPool<Block> BlockPool { get; private set; }

        private List<Ring> rings;
        private int ringsSpawned;
        private float timeToNextRingSpawn;

        private void Start()
        {
            EventManager.activate += Activate;
            EventManager.gameUpdate += OnGameUpdate;
            EventManager.reset += OnReset;
            EventManager.broadcastLevel += SetLevelData;
        }

        private void OnDestroy()
        {
            EventManager.activate -= Activate;
            EventManager.gameUpdate -= OnGameUpdate;
            EventManager.reset -= OnReset;
            EventManager.broadcastLevel -= SetLevelData;
        }

        private void OnGameUpdate()
        {
            timeToNextRingSpawn -= Time.deltaTime;

            if (timeToNextRingSpawn <= 0 && ringsSpawned < data.RingCount)
            {
                if (ringsSpawned >= data.MaxRings)
                {
                    EventManager.maxRingsReached?.Invoke("You didn't break the blocks in time...'");
                }

                SpawnRing();
                ringsSpawned++;

                timeToNextRingSpawn = data.TimeBetweenRings;
            }
        }

        private void OnReset()
        {
            BlockPool?.ReclaimAll();
            timeToNextRingSpawn = 0;
            ringsSpawned = 0;
        }

        private void SetLevelData(LevelData data)
        {
            OnReset();

            this.data = data;
        }

        private void Activate()
        {
            rings = new List<Ring>();

            if (BlockPool == null)
            {
                BlockPool = new MultiPrefabPool<Block>(data.BlockCountRing, transform, data.StandardBlock);
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
            if (ringsSpawned == data.RingCount && rings.Count == 0)
            {
                EventManager.spawnerExhausted?.Invoke();
            }
        }
    }
}