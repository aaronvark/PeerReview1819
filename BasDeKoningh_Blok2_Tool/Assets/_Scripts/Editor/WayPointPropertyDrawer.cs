using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Assertions;

namespace EasyAI
{
    [CustomEditor(typeof(WayPointData)), CanEditMultipleObjects]
    public class WayPointPropertyDrawer : Editor
    {
        protected virtual void OnSceneGUI()
        {

        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            WayPointData wayPointData = (WayPointData)target;
            if(GUILayout.Button("Create Path"))
            {
                wayPointData.spline.Reset();
                GameObject mySpline = GameObject.Instantiate(wayPointData.spline, wayPointData.transform).gameObject;
                Selection.SetActiveObjectWithContext(mySpline, mySpline);
            }
            //We moeten als we dit script open hebben staan in de editor window
            //Het spline game object wat net geinstantieerd is als selectie houden in de scene
        }        /*
        Vector3 found = Vector3.zero;
        Vector3 newHandle = Vector3.zero;
        Vector3 selectedPosition = Vector3.zero;
        private WayPointData wayPointData;
        protected virtual void OnSceneGUI()
        {
            List<Vector3> handlePositions = new List<Vector3>();
            handleTransform = wayPointData.transform;
            Vector3 p0 = ShowPoint(0);
            for(int i = 1; i < wayPointData.GetWayPointCount; i += 3)
            {
                Vector3 p1 = ShowPoint(i);
                Vector3 p2 = ShowPoint(i + 1);
                Vector3 p3 = ShowPoint(i + 2);

                Handles.color = Color.gray;
                Handles.DrawLine(p0, p1);
                Handles.DrawLine(p2, p3);

                Handles.DrawBezier(p0, p3, p1, p2, Color.white, null, 2f);
                p0 = p3;
            }
            EditorGUI.BeginChangeCheck();
            //Initialize the handles
            foreach(Vector3 position in wayPointData.WayPoints)
            {
                if(!handlePositions.Contains(Handles.PositionHandle(position, Quaternion.identity)))
                {
                    handlePositions.Add(Handles.PositionHandle(position, Quaternion.identity));
                }
            }

            //Any handle die al een waypoint vector3 heeft

            //New waypointss
            Vector3 newTargetPosition = Handles.PositionHandle(wayPointData.NewWayPointPosition, Quaternion.identity);
            
            if (selectedPosition != newTargetPosition)
            {
                Debug.Log(selectedPosition);
                found = wayPointData.WayPoints.Find(wp => wp.Equals(selectedPosition));
                //found = selectedPosition;
                newHandle = Handles.PositionHandle(found, Quaternion.identity);
                //moving the handle
            }
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(wayPointData, "Setting waypoint positions");
                wayPointData.NewWayPointPosition = newTargetPosition;
            }
        }

        private const float handleSize = 0.04f;
        private const float pickSize = 0.06f;
        private int selectedIndex = -1;
        private Transform handleTransform;

        private Vector3 ShowPoint(int index)
        {
            Vector3 point = handleTransform.TransformPoint(wayPointData.GetWayPointPoint(index));
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

        public override void OnInspectorGUI()
        {
            wayPointData = (WayPointData)target;

            if (selectedIndex >= 0 && selectedIndex < wayPointData.GetWayPointCount)
            {
                DrawSelectedPoint();
            }
            if (GUILayout.Button("Add WayPoint"))
            {              
                wayPointData.WayPoints.Add(wayPointData.NewWayPointPosition);
            }
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
        }*/
    }
}

