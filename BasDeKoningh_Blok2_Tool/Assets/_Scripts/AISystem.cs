using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyAI

{ 
    public enum Mood
    {
        Aggresive = 0,
        AtEase = 1,
        Sleeping = 2
    }

    public enum Confidence
    {
        Leader = 0,
        Mediocre = 1,
        Pussy = 2
    }

    public enum WanderType
    {
        Dynamic = 0,
        Static = 1,
        Fixed = 2
    }

    public enum CombatStyle
    {
        Offensive = 0,
        Defensive = 1,
        InBetween = 2
    }


    public struct TemperamentData
    {
        public Mood Mood;
        public Confidence Confidence;
        public WanderType WanderType;
        public CombatStyle CombatStyle;
    }

    public class AISystem : MonoBehaviour
    {
        [SerializeField] private List<ScriptableSetting> scriptableSettings;
        public List<ScriptableSetting> ScriptableSettings { get => scriptableSettings; set => scriptableSettings = value; }

        private void OnEnable()
        {
            ScriptableSettings = Resources.LoadAll<ScriptableSetting>("Settings/").ToList();
            EventManager<ScriptableSetting>.AddHandler(EVENT.show, ShowSetting);
        }

        public void ShowSetting(ScriptableSetting setting)
        {

        }
    }
}