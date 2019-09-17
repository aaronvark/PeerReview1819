using UnityEngine;

namespace Breakin.GameManagement
{
    [CreateAssetMenu(fileName = "NewLevel", menuName = "Breaking/Level data asset")]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private int ringCount;
        public int RingCount => ringCount;
    }
}