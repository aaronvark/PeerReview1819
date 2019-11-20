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


        private void OnDrawGizmos()
        {
            // Draw a yellow sphere at the transform's position
            if (TemperamentManager.CombatTriggerRange(CombatStyle) != 0)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(transform.position, TemperamentManager.CombatTriggerRange(CombatStyle));
            }
            if (TemperamentManager.MoodTriggerRange(Mood) != 0)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, TemperamentManager.MoodTriggerRange(Mood));
            }

        }
    }
}
