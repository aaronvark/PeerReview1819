using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyAI
{
    public class ScriptableNPC : ScriptableObject
    {
        [SerializeField] private string npcId;
        [SerializeField] private string npcName;
        [SerializeField] private NPCType npcType;
        [SerializeField] private GameObject prefab;
        public string NpcId { get => npcId; set => npcId = value; }
        public string NpcName { get => npcName; set => npcName = value; }
        public NPCType NpcType { get => npcType; set => npcType = value; }
        public GameObject Prefab { get => prefab; set => prefab = value; }

        public List<ScriptableSetting> settings;

    }
}
