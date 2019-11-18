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
        private bool minalize = false;
        private Node instance;

        public Node(int id)
        {
            this.id = id;

            nodeInfo = new NodeInfo(this);
            instance = this;
        }

        public void OnGUI()
        {
            if (instance != null)
            {
                if (!minalize)
                    maxNodeSize = GUI.Window(id, maxNodeSize, DragNode, nodeInfo.ClassName + " node");
                else
                    minNodeSize = GUI.Window(id, minNodeSize, DragNode, nodeInfo.ClassName + " node");
            }
        }

        public void DragNode(int id)
        {
            if (!minalize)
                scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false);
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();

            if (!minalize)
                if (GUILayout.Button("X", GUILayout.Width(20)))
                    instance = null;

            if (minalize)
            {
                GUILayout.Label(nodeInfo.ClassName);
                if (GUILayout.Button("+", GUILayout.MaxWidth(20)))
                    minalize = !minalize;
            }
            else
            {
                if (GUILayout.Button("-", GUILayout.MaxWidth(20)))
                    minalize = !minalize;
            }
            GUILayout.EndHorizontal();


            if (!minalize)
                nodeInfo.Draw();

            GUILayout.EndVertical();
            if (!minalize)
                GUILayout.EndScrollView();
            GUI.DragWindow();
        }



    }
}