using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EasyAI
{
    [System.Serializable]
    public class WayPointData : MonoBehaviour, ISetting
    {
        [SerializeField]
        public WanderType WanderType;
        [SerializeField]
        public List<Vector3> WayPoints;
        [SerializeField]
        public int WayPointIndex = 0;

        public int GetWayPointCount { get { return WayPoints.Count; } }
        public Vector3 GetWayPointPoint(int index) { return WayPoints[index]; }
        public void SetWayPoint(int index, Vector3 point) { WayPoints[index] = point; }

        public void RenderUI(SerializedProperty property)
        {
            var wanderType = property.FindPropertyRelative("WanderType");
            var wayPoints = property.FindPropertyRelative("WayPoints");
            EditorGUILayout.PropertyField(wanderType);
            EditorGUILayout.PropertyField(wayPoints);
        }

        public System.Type GetChildType()
        {
            return this.GetType();
        }

        void OnDrawGizmos()
        {
            // Draw a yellow sphere at the transform's position
            Gizmos.color = Color.yellow;
            if (WayPoints != null && WayPoints.Count > 1)
            {
                foreach (Vector3 point in WayPoints)
                {
                    Gizmos.DrawSphere(point, 0.2f);
                }
            }

        }
    }
}