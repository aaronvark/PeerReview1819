using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace UnityEngine.Scripting.UML
{
    public class Node
    {
        public Vector2 scrollPosition = Vector2.zero;
        public Rect minNodeSize = new Rect(30, 30, 175, 40);
        public Rect maxNodeSize = new Rect(30, 30, 225, 250);
        public NodeInfo nodeInfo;
        public int id;
        public bool minalized = false;
        public Node instance;
        private static Node selectedNode;

        public Node(int id)
        {
            this.id = id;

            nodeInfo = new NodeInfo(this);
            instance = this;
        }

        private void IsSelected()
        {
            selectedNode = this;
        }

        public void OnGUI()
        {
            if (instance != null)
            {
                Event e = Event.current;

                if ((!minalized && maxNodeSize.Contains(e.mousePosition)) || (minalized && minNodeSize.Contains(e.mousePosition)))
                    IsSelected();

                if (!minalized)
                    maxNodeSize = GUI.Window(id, maxNodeSize, DragNode, nodeInfo.ClassName + " node");
                else
                    minNodeSize = GUI.Window(id, minNodeSize, DragNode, nodeInfo.ClassName + " node");
            }
        }

        public void DragNode(int id)
        {
            if (!minalized)
                scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false);
         
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();

            if (!minalized)
                if (GUILayout.Button("X", GUILayout.Width(20)))
                    instance = null;

            if (minalized)
            {
                GUILayout.Label(nodeInfo.ClassName);
                if (GUILayout.Button("+", GUILayout.MaxWidth(20)))
                    minalized = !minalized;
            }
            else
            {
                if (GUILayout.Button("-", GUILayout.MaxWidth(20)))
                    minalized = !minalized;
            }
            GUILayout.EndHorizontal();


            if (!minalized)
                nodeInfo.Draw();
            GUILayout.EndVertical();
            if (!minalized)
                GUILayout.EndScrollView();

            GUI.DragWindow();
        }
    }
}