using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

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
            //this.data = serializedObject.FindProperty("settings");
            //((ISetting)data.GetArrayElementAtIndex((int)currenOpenSetting).objectReferenceValue).RenderUI(data.GetArrayElementAtIndex((int)currenOpenSetting));
            //TODO
            //We hebben instances van alle classes nodig om de objectReference op te halen

            switch ((int)currenOpenSetting)
            {
                case 0:
                    this.data = serializedObject.FindProperty("settings");
                    Debug.Log(this.data.isArray);
                    Debug.Log(data.GetArrayElementAtIndex(0));
                    ISetting setting = data.GetArrayElementAtIndex(0).objectReferenceValue as ISetting;
                    Debug.Log(setting);
                    setting.RenderUI(data.GetArrayElementAtIndex(0));
                    //ISetting ding = ((ISetting)data.GetArrayElementAtIndex(0).objectReferenceValue);
                    //((ISetting)data.GetArrayElementAtIndex(0).objectReferenceValue).RenderUI(data.GetArrayElementAtIndex(0));
                    break;
                case 1:
                    break;
                default:
                    break;
            }

            /*
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
            }*/

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
