using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyAI
{
    public enum SettingType
    {
        Temperament = 0,
        AISettings = 1,
        Animations = 2,
        Sounds = 3,
        WayPoints = 4
    }
    public class ScriptableSetting : ScriptableObject
    {
        [SerializeField] private string jsonSettingString;
        public string JsonSettingString { get => jsonSettingString; set => jsonSettingString = value; }



        [SerializeField] private SettingType settingType;
        public SettingType SettingType { get => settingType; set => settingType = value; }


    }
}
