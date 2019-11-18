using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace EasyAI
{
    [CustomEditor(typeof(AISystem))]
    public class AISystemEditor : Editor
    {
        SerializedProperty settings;
        SerializedProperty data;

        List<SerializedProperty> allSettings;
        SerializedObject theTarget;
        AISystem aiSystem;
        Object scriptableObject;
        System.Type objectType;
        SettingType currenOpenSetting;
        private void OnEnable()
        {
            theTarget = new SerializedObject(target);
            aiSystem = target as AISystem;
            settings = theTarget.FindProperty("settingsData");
            data = theTarget.FindProperty("temperamentData");
            EventManager<SettingType>.AddHandler(EVENT.show, ChangeOpenSetting);
        }

        public void ChangeOpenSetting(SettingType incomingSettingType)
        {
            currenOpenSetting = incomingSettingType;
        }

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();
            
            if (GUILayout.Button("Temperament"))
            {
                //load temperament settings and show temperament settings
                EventManager<SettingType>.BroadCast(EVENT.show, SettingType.Temperament);
                //objectTyp = aiSystem.temperamentData;
            }
            if (GUILayout.Button("AISettings"))
            {
                //load AISettings and show
                EventManager<SettingType>.BroadCast(EVENT.show, SettingType.AISettings);
            }
            if (GUILayout.Button("Animations"))
            {
                //load Animation settings and show
                EventManager<SettingType>.BroadCast(EVENT.show, SettingType.Animations);
            }
            if (GUILayout.Button("WayPoints"))
            {
                //loand and show waypoint settings
                EventManager<SettingType>.BroadCast(EVENT.show, SettingType.WayPoints);

            }
            if (GUILayout.Button("Sounds"))
            {
                //load sound settings and show
                EventManager<SettingType>.BroadCast(EVENT.show, SettingType.Sounds);
            }

            if (theTarget != null)
                theTarget.Update();
            else
            {
                theTarget = new SerializedObject(target);
                settings = theTarget.FindProperty("settingBases");
                data = theTarget.FindProperty("temperamentData");
                EventManager<SettingType>.AddHandler(EVENT.show, ChangeOpenSetting);
            }
            /*0
             * Create enum die kijkt welke propertie die wilt uitlezen.
             * in elke button zet je de waarde en aan de hand daarvan.
             */
            switch(currenOpenSetting)
            {
                case SettingType.Temperament:
                    this.data = serializedObject.FindProperty("temperamentData");
                    foreach(var child in this.data.GetChildren())
                    {
                        EditorGUILayout.PropertyField(child);
                    }
                    serializedObject.ApplyModifiedProperties();
                    break;
                case SettingType.AISettings:
                    this.data = serializedObject.FindProperty("aiSettingData");
                    foreach (var child in this.data.GetChildren())
                    {
                        EditorGUILayout.PropertyField(child);
                    }
                    serializedObject.ApplyModifiedProperties();
                    break;
                case SettingType.Animations:
                    this.data = serializedObject.FindProperty("animationData");
                    foreach (var child in this.data.GetChildren())
                    {
                        EditorGUILayout.PropertyField(child);
                    }
                    serializedObject.ApplyModifiedProperties();
                    break;
                case SettingType.WayPoints:
                    this.data = serializedObject.FindProperty("wayPointData");
                    foreach (var child in this.data.GetChildren())
                    {
                        EditorGUILayout.PropertyField(child);
                    }
                    serializedObject.ApplyModifiedProperties();
                    break;
                case SettingType.Sounds:
                    this.data = serializedObject.FindProperty("wayPointData");
                    foreach (var child in this.data.GetChildren())
                    {
                        EditorGUILayout.PropertyField(child);
                    }
                    serializedObject.ApplyModifiedProperties();
                    break;
                default:
                    return;
            }

            if (GUILayout.Button("Save Prefab!"))
            {
                //Save the prefab
                GameObject prefab = PrefabUtility.SaveAsPrefabAsset(aiSystem.gameObject, "Assets/_Prefabs/NPCS/" + aiSystem.gameObject.name + ".prefab");
            }
            //Apply the changes to our list
            theTarget.ApplyModifiedProperties();
        
            
        }
    }
}
