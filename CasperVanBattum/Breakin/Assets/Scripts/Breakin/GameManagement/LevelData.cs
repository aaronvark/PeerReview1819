using System;
using Breakin.Behaviour;
using UnityEngine;
using UnityEngine.Assertions;

namespace Breakin.GameManagement
{
    [CreateAssetMenu(fileName = "NewLevel", menuName = "Breakin/Level data asset")]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private Block standardBlock; // Block prefab 
        public Block StandardBlock => standardBlock;
        [SerializeField] private float spawnRadius = 3; // Radius of the most recent ring
        public float SpawnRadius => spawnRadius;
        [SerializeField] private int blockCountRing = 10; // Block count in each ring
        public int BlockCountRing => blockCountRing;
        [SerializeField] private int ringCount = 5; // Number of rings to spawn
        public int RingCount => ringCount;

        [SerializeField]
        private int maxRings = 7; // When a ring is spawned but this var is already reached, it's game over

        public int MaxRings => maxRings;
        [SerializeField] private float ringSpacing = .3f; // Distance between each ring (center to center)
        public float RingSpacing => ringSpacing;
        [SerializeField] private float timeBetweenRings = 30; // In secconds
        public float TimeBetweenRings => timeBetweenRings;
        [SerializeField] private int lives = 3; // Player lives (number of chances - 1)
        public int Lives => lives;

        private void OnValidate()
        {
            Assert.IsNotNull(standardBlock, nameof(standardBlock) + " was not set in level " + name + ".");
        }
    }
}