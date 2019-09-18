using Breakin.Behaviour;
using UnityEngine;

namespace Breakin.GameManagement
{
    [CreateAssetMenu(fileName = "NewLevel", menuName = "Breaking/Level data asset")]
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
        [SerializeField] private int maxRings; // When a ring is spawned but this number is already reached, it's game over
        public int MaxRings => maxRings;
        [SerializeField] private float ringSpacing = .3f; // Distance between each ring (center to center)
        public float RingSpacing => ringSpacing;
        [SerializeField] private float timeBetweenRings = 30; // In secconds
        public float TimeBetweenRings => timeBetweenRings;
        [SerializeField] private int lives; // Player lives (number of chances - 1)
        public int Lives => lives;
    }
}