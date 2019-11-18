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
        int id = 0;

        [MenuItem("Custom Tools/UML Window #u")]
        private static void Init()
        {
            NodeWindow umlWindow = new NodeWindow();
            umlWindow.titleContent = new GUIContent("UML Creator");
            GUIContent icon = EditorGUIUtility.IconContent("animationdopesheetkeyframe");
            umlWindow.titleContent.image = icon.image;
            umlWindow.Show();
            umlWindow.grid = new Grid(umlWindow);
        }

        // Update is called once per frame
        private void OnGUI()
        {
            BeginWindows();
            
            #region Drawing grid source: http://gram.gs/gramlog/creating-node-based-editor-unity/
                grid.UpdateGrid();
            #endregion

            //Show Nodes
            for (int i = 0; i < nodes.Count; i++)
                nodes[i].OnGUI();

            Event e = Event.current;
            if(e.type == EventType.ContextClick)
                CreateGenericMenu();

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
            ClassWriter cw = new ClassWriter();

            for (int i = 0; i < nodes.Count; i++)
                cw.GenerateClass(nodes[i].nodeInfo);
        }

        /// <summary>
        /// Creating the Generic left click menu.
        /// </summary>
        public void CreateGenericMenu()
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Add node"), false, AddNode);
            menu.AddItem(new GUIContent("Generate nodes"), false, GenerateNodes);
            menu.ShowAsContext();
        }
    }
}