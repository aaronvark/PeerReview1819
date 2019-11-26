using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Assertions;

namespace EasyAI
{
    [CustomEditor(typeof(WayPointData)), CanEditMultipleObjects]
    public class WayPointDataEditorWindow : Editor
    {
        private WayPointData wayPointData;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            wayPointData = (WayPointData)target;
            if (wayPointData.WayPoints.Count< 1)
            {
                InitPath(wayPointData);
            }
            if (GUILayout.Button("Add Point"))
            {
                InitPath(wayPointData);            
            }
            if (GUILayout.Button("Save Prefab!"))
            {
                //Save the prefab
                GameObject prefab = PrefabUtility.SaveAsPrefabAsset(wayPointData.gameObject, "Assets/_Prefabs/NPCS/" + wayPointData.gameObject.name + ".prefab");
            }
        }        

        private void InitPath(WayPointData wayPointData)
        {
            var position = wayPointData.transform.position;
            wayPointData.WayPoints.Add(new Vector3(position.x + 1, position.y + 1, position.z + 1));
        }

        protected virtual void OnSceneGUI()
        {
            wayPointData = (WayPointData)target;
            if (wayPointData.WayPoints.Count < 1) return;

            EditorGUI.BeginChangeCheck();

            for (int i = 0; i < wayPointData.WayPoints.Count; i++)
            {
                //ShowPoint(i, handlePositions[i]);
                //GUI.SetNextControlName((i+1).ToString());
                selectedIndex = i;
                wayPointData.WayPoints[i] = Handles.PositionHandle(wayPointData.WayPoints[i], Quaternion.identity);
            }
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(wayPointData, "Setting waypoint positions");
                //wayPointData.WayPoints[selectedIndex] = handlePositions[selectedIndex];
            }
        }

        /****/
        //Trying out handle buttons ( Currently not using this code )
        private const float handleSize = 0.04f;
        private const float pickSize = 0.06f;
        private int selectedIndex = -1;
        private Transform handleTransform;

        private Vector3 ShowPoint(int index, Vector3 point)
        {
            Handles.color = Color.white;
            float size = HandleUtility.GetHandleSize(point);

            if (Handles.Button(point, Quaternion.identity, size *handleSize, size *pickSize, Handles.SphereHandleCap))
            {
                selectedIndex = index;
                Repaint();
            }
            if (selectedIndex == index)
            {
                EditorGUI.BeginChangeCheck();
                point = Handles.DoPositionHandle(point, Quaternion.identity);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(wayPointData, "Move Point");
                    EditorUtility.SetDirty(wayPointData);
                    wayPointData.SetWayPoint(index, handleTransform.InverseTransformPoint(point));
                }
            }
            return point;
        }

        private void DrawSelectedPoint()
        {
            GUILayout.Label("Selected Point");
            EditorGUI.BeginChangeCheck();
            Vector3 point = EditorGUILayout.Vector3Field("Position", wayPointData.GetWayPointPoint(selectedIndex));
            if(EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(wayPointData, "Move Point");
                EditorUtility.SetDirty(wayPointData);
                wayPointData.SetWayPoint(selectedIndex, point);
            }
        }
        /****/
    }
}

