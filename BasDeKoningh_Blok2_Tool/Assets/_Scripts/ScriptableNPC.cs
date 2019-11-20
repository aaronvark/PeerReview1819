using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace EasyAI
{
    [System.Serializable]
    public class ScriptableNPC : ScriptableObject
    {
        [SerializeField] private string npcId;
        [SerializeField] private string npcName;
        [SerializeField] private NPCType npcType;
        [SerializeField] private GameObject model;
        [SerializeField] private AnimatorController animatorController;
        [SerializeField] private Transform spawnPosition;
        [SerializeField] private Object aiSystem;
        public string NpcId { get => npcId; set => npcId = value; }
        public string NpcName { get => npcName; set => npcName = value; }
        public NPCType NpcType { get => npcType; set => npcType = value; }
        public GameObject Model { get => model; set => model = value; }
        public AnimatorController AnimatorController { get => animatorController; set => animatorController = value; }
        public Transform SpawnPosition { get => spawnPosition; set => spawnPosition = value; }
        public Object AISystem { get => aiSystem; set => aiSystem = value; }

        [SerializeField] public List<Object> settings;

    }
}
