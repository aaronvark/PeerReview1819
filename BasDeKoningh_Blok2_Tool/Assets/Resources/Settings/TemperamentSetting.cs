using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyAI
{
    public class TemperamentSetting : ScriptableSetting
    {
        [SerializeField] private bool show;
        public bool Show { get => show; set => show = value; }

        public Mood Mood;
        public Confidence Confidence;
        public WanderType WanderType;
        public CombatStyle CombatStyle;
    }
}
