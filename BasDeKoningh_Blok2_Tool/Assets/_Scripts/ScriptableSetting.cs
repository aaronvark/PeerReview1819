using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EasyAI
{
    public enum SettingType
    {
        Temperament = 0,
        AISettings = 1,
        Animations = 2,
        WayPoints = 3,
        Sounds = 4
    }
    public class ScriptableSetting : ScriptableObject
    {
        [SerializeField] private string jsonSettingString;
        public string JsonSettingString { get => jsonSettingString; set => jsonSettingString = value; }

        [SerializeField] private ScriptableObject currentSetting;
        

        [SerializeField] private SettingType settingType;
        public SettingType SettingType { get => settingType; set => settingType = value; }

        private Dictionary<int, ScriptableObject> settingsData = new Dictionary<int, ScriptableObject>();
        private void OnEnable()
        {
            //settingsData.Add(0, new TemperamentData());
            //settingsData.Add(1, new AiSettings());
            //currentSetting = settingsData[(int)settingType];
            
        }

        private void OnValidate()
        {
            //currentSetting = settingsData[(int)settingType];
        }
    }
}
