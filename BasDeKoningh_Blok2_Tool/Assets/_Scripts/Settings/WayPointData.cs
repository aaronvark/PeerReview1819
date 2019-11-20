using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

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

        private void Start()
        {
            foreach(Transform point in WayPoints)
            {
                point.parent = null;
            }
            //EventManager<MonoBehaviour>.BroadCast(EVENT.setBehaviour, this);

        }

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
            if(WayPoints != null)
            {
                foreach (Transform point in WayPoints)
                {
                    Gizmos.DrawSphere(point.position, 0.2f);
                }
            }
            
        }
    }
}