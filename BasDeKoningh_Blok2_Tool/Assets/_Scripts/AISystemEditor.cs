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
                    List<SerializedProperty> items = new List<SerializedProperty>();
                    for (int j = 0; j < setting.arraySize; j++)
                    {
                        items.Add(setting.GetArrayElementAtIndex(j));
                    }
                    foreach(SerializedProperty property in items)
                    {
                        //Draw the properties of the active settings
                        EditorGUILayout.LabelField("Properties:");
                        EditorGUILayout.PropertyField(property);
                        EditorGUILayout.PropertyField(property);
                        EditorGUILayout.PropertyField(property);
                        EditorGUILayout.PropertyField(property);


                        EditorGUILayout.Space();
                    }
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
