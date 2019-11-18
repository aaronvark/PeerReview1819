using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace EasyAI
{
    [System.Serializable]
    public class TemperamentData : MonoBehaviour, ISetting
    {
        [SerializeField]
        public Mood Mood;
        [SerializeField]
        public Confidence Confidence;
        [SerializeField]
        public WanderType WanderType;
        [SerializeField]
        public CombatStyle CombatStyle;


        public void RenderUI(SerializedProperty property)
        {
            Debug.Log(this.GetType().Assembly);
            var mood = property.FindPropertyRelative("Mood");
            var confidence = property.FindPropertyRelative("Confidence");
            var wanderType = property.FindPropertyRelative("WanderType");
            var combatStyle = property.FindPropertyRelative("CombatStyle");
            EditorGUILayout.PropertyField(mood);
            EditorGUILayout.PropertyField(confidence);
            EditorGUILayout.PropertyField(wanderType);
            EditorGUILayout.PropertyField(combatStyle);
        }

        public System.Type GetChildType()
        {
            return this.GetType();
        }
    }
}
