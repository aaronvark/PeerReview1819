using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyAI
{
    [System.Serializable]
    public class AiSettingData
    {
        [SerializeField]
        public float Damage;
        [SerializeField]
        public float Health;
        [SerializeField]
        public float Range;
        [SerializeField]
        public AiType AiType;
    }
}
