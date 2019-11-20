using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace UnityEngine.Scripting.UML
{
    public class NodeWindow : EditorWindow
    {
        private Grid grid;
        private List<Node> nodes = new List<Node>();
        private Inheritance inheritance;
        int id = 0;
        private delegate void DrawInheritance();
        private event DrawInheritance drawInheritance;
        private bool SetInheritance = false;

        [MenuItem("Custom Tools/UML Window #u")]
        private static void Init()
        {
            NodeWindow umlWindow = new NodeWindow();
            umlWindow.titleContent = new GUIContent("UML Creator");
            GUIContent icon = EditorGUIUtility.IconContent("animationdopesheetkeyframe");
            umlWindow.titleContent.image = icon.image;
            umlWindow.Show();
            umlWindow.grid = new Grid(umlWindow);
            umlWindow.inheritance = new Inheritance();
        }

        // Update is called once per frame
        private void OnGUI()
        {
            Handles.BeginGUI();
            BeginWindows();

            #region Drawing grid source: http://gram.gs/gramlog/creating-node-based-editor-unity/
            if (grid != null)
                grid.UpdateGrid();
            else
                grid = new Grid(this);
            #endregion

            //Show Nodes
            for (int i = 0; i < nodes.Count; i++)
                nodes[i].OnGUI();

            Event e = Event.current;
            if (e.type == EventType.ContextClick)
                CreateGenericMenu();

            drawInheritance?.Invoke();
            EndWindows();
        }

        private void AddNode()
        {
            id++;
            nodes.Add(new Node(id));
        }

        /// <summary>
        /// Generate Classes
        /// </summary>
        private void GenerateNodes()
        {
            ICodeGenerator cw = new ClassWriter();

            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].instance != null)
                    cw.GenerateClass(nodes[i].nodeInfo);
            }
        }

        private void BeginDrawingInheritance()
        {
            inheritance.SetInheritance(nodes);
            drawInheritance += inheritance.DrawInheritances;

        }

        /// <summary>
        /// Creating the Generic left click menu.
        /// </summary>
        public void CreateGenericMenu()
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Add class"), false, AddNode);
            menu.AddItem(new GUIContent("Generate class"), false, GenerateNodes);
            menu.AddItem(new GUIContent("Set Inheritance"), SetInheritance, BeginDrawingInheritance);
            menu.ShowAsContext();
        }
    }
}