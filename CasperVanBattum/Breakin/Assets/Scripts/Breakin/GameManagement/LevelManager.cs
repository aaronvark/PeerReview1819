using UnityEngine;
using UnityEngine.Assertions;

namespace Breakin.GameManagement
{
    [System.Serializable]
    public class LevelManager
    {
        [SerializeField] private LevelData[] levels;

        private int nextLevel;

        public LevelData GetNextLevel()
        {
            // There must be levels added to the levelmanager for the game to run
            Assert.IsTrue(levels.Length > 0);

            // Return null if there is no next level, else return the next level
            return nextLevel >= levels.Length ? null : levels[nextLevel++];
        }

        public void Reset()
        {
            nextLevel = 0;
        }
    }
}