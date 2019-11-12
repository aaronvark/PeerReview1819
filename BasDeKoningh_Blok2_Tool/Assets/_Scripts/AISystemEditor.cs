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
        SerializedObject theTarget;
        private void OneEnable()
        {
            theTarget = new SerializedObject(target);
            settings = theTarget.FindProperty("scriptableSettings");
        }

        public override void OnInspectorGUI()
        {
            theTarget.Update();

            //Display our settings to the inspector window

            for (int i = 0; i < settings.arraySize; i++)
            {
                SerializedProperty setting = settings.GetArrayElementAtIndex(i);
                SerializedProperty check = setting.FindPropertyRelative("Show");
                if (check.boolValue)
                {
                    SerializedProperty item0 = setting.FindPropertyRelative("Mood");
                    SerializedProperty item1 = setting.FindPropertyRelative("Confidence");
                    SerializedProperty item2 = setting.FindPropertyRelative("WanderType");
                    SerializedProperty item3 = setting.FindPropertyRelative("CombatStyle");

                    //2 : Full custom GUI Layout <-- Choose me I can be fully customized with GUI options.
                    EditorGUILayout.LabelField("Customizable Field With GUI");
                    EditorGUILayout.PropertyField(item0);
                    EditorGUILayout.PropertyField(item1);
                    EditorGUILayout.PropertyField(item2);
                    EditorGUILayout.PropertyField(item3);


                    EditorGUILayout.Space();
                }
                else
                {
                    continue;
                }

             
            }

            //Apply the changes to our list
            theTarget.ApplyModifiedProperties();
        
            base.OnInspectorGUI();
            if (GUILayout.Button("Temperament"))
            {
                //load temperament settings and show temperament settings

                
            }
            if (GUILayout.Button("AISettings"))
            {
                //load AISettings and show
            }
            if (GUILayout.Button("Animations"))
            {
                //load Animation settings and show
            }
            if (GUILayout.Button("Sounds"))
            {
                //load sound settings and show
            }
            if (GUILayout.Button("WayPoints"))
            {
                //loand and show waypoint settings
            }
        }
    }
}
