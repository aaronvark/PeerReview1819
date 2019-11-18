using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace EasyAI
{
    [System.Serializable]
    public class WayPointData : MonoBehaviour, ISetting
    {
        [SerializeField]
        public WanderType WanderType;
        [SerializeField]
        public List<Transform> WayPoints;
        [SerializeField]
        public int WayPointIndex = 0;

        public void RenderUI(SerializedProperty property)
        {
            Debug.Log(this.GetType().Assembly);

            var wanderType = property.FindPropertyRelative("WanderType");
            var wayPoints = property.FindPropertyRelative("WayPoints");
            EditorGUILayout.PropertyField(wanderType);
            EditorGUILayout.PropertyField(wayPoints);
        }


        public System.Type GetChildType()
        {
            return this.GetType();
        }
    }
}