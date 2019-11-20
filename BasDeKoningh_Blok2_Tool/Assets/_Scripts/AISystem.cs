using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

    public enum AiType
    {
        Close = 0,
        Ranged = 1,
        Medic = 2
    }
    
    

    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Animator))]
    public class AISystem : MonoBehaviour
    {
        [HideInInspector]private Dictionary<int, Object> settingsData = new Dictionary<int, Object>();

        [SerializeField]
        private ScriptableNPC thisNpc;
        [SerializeField]
        public TemperamentData temperamentData;
        [SerializeField]
        public AiSettingData aiSettingData;
        [SerializeField]
        public AnimationData animationData;
        [SerializeField]
        public WayPointData wayPointData;

        [SerializeField]public List<Component> settings = new List<Component>();

        public void InitAI()
        {
            temperamentData = new TemperamentData();
            aiSettingData = new AiSettingData();
            animationData = new AnimationData();
            wayPointData = new WayPointData();

        }

        public void ShowSetting(SettingType settingType)
        {
            //EventManager<Object>.BroadCast(EVENT.setSettings, settingsData[(int)settingType]/*thisNpc.settings.Find(s => s.SettingType.Equals(settingType))*/);

            /*
            switch (settingType)
            {
                case SettingType.Temperament:
                    
                    if (!settingBases.Contains(temperamentData))
                        settingBases.Add(temperamentData);
                    settingBases.Find(sb => sb.Equals(temperamentData)).ShowSetting();
                    temperamentData.ShowSetting();
                    break;
                case SettingType.AISettings:
                    if (!settingBases.Contains(aiSettings))
                        settingBases.Add(aiSettings);
                    settingBases.Find(sb => sb.Equals(aiSettings)).ShowSetting();
                    break;
                default:
                    if (!settingBases.Contains(temperamentData))
                        settingBases.Add(temperamentData);
                    settingBases.Find(sb => sb.Equals(temperamentData)).ShowSetting();
                    break;
            }*/
        }

        public void GiveNpcData(ScriptableNPC npc)
        {
            thisNpc = ScriptableObject.Instantiate(npc);
            this.gameObject.name = thisNpc.NpcName;
            for (int i = 0; i < thisNpc.settings.Count; i++)
            {
                //if(!settings.Contains(thisNpc.settings[i] as ISetting))
                MonoScript monoScript = thisNpc.settings[i] as MonoScript;
                var comp = GetComponent(monoScript.GetClass());
                settings.Add(comp);
            }
        }
    }
}