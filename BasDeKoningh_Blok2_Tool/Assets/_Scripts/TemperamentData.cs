using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyAI
{
    [System.Serializable]
    public class TemperamentData
    {
        [SerializeField]
        public Mood Mood;
        [SerializeField]
        public Confidence Confidence;
        [SerializeField]
        public WanderType WanderType;
        [SerializeField]
        public CombatStyle CombatStyle;
    }
}
