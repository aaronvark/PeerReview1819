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
