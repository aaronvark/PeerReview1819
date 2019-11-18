﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyAI
{
    [System.Serializable]
    public class ScriptableNPC : ScriptableObject
    {
        [SerializeField] private string npcId;
        [SerializeField] private string npcName;
        [SerializeField] private NPCType npcType;
        [SerializeField] private GameObject prefab;
        [SerializeField] private Transform spawnPosition;
        [SerializeField] private AISystem aiSystem;
        public string NpcId { get => npcId; set => npcId = value; }
        public string NpcName { get => npcName; set => npcName = value; }
        public NPCType NpcType { get => npcType; set => npcType = value; }
        public GameObject Prefab { get => prefab; set => prefab = value; }
        public Transform SpawnPosition { get => spawnPosition; set => spawnPosition = value; }
        public AISystem AISystem { get => aiSystem; set => aiSystem = value; }

        [SerializeField] public List<Object> settings;

    }
}
