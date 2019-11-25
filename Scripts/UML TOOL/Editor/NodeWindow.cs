using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace UnityEngine.Scripting.UML
{
    public class NodeWindow : EditorWindow
    {
        private Grid grid;
        private List<Node> nodes;
        private Inheritance inheritance;
        private int id = 0;
        private delegate void DrawInheritance();
        private event DrawInheritance drawInheritance;
        private bool setInheritance = false;
        private SaveNodes saveNode;

        [MenuItem("Custom Tools/UML Window #u ")]
        private static void Init()
        {
            NodeWindow umlWindow = new NodeWindow();

            try
            {
                umlWindow.saveNode = (SaveNodes)AssetDatabase.LoadAssetAtPath("Assets/SaveNode.asset", typeof(SaveNodes));
                umlWindow.nodes = umlWindow.saveNode.Nodes;
                umlWindow.inheritance = umlWindow.saveNode.Inheritance;

                if (umlWindow.setInheritance)
                    umlWindow.drawInheritance();
            } 
            catch
            {
                umlWindow.saveNode = ScriptableObject.CreateInstance(typeof(SaveNodes)) as SaveNodes;
                AssetDatabase.CreateAsset(umlWindow.saveNode, "Assets/SaveNode.asset");
                umlWindow.inheritance = new Inheritance();
                umlWindow.nodes = new List<Node>();
            }

            umlWindow.titleContent = new GUIContent("UML Creator");
            GUIContent icon = EditorGUIUtility.IconContent("animationdopesheetkeyframe");
            umlWindow.titleContent.image = icon.image;

            umlWindow.Show();
            umlWindow.grid = new Grid(umlWindow);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
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

            Event e = Event.current;
            if (e.type == EventType.ContextClick)
                CreateGenericMenu();

            //Show Nodes
            if (nodes.Count > 0)
                for (int i = 0; i < nodes.Count; i++)
                    nodes[i].OnGUI();


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
            if (nodes.Count < 2)
                return;

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
            menu.AddItem(new GUIContent("Set Inheritance"), setInheritance, BeginDrawingInheritance);
            menu.ShowAsContext();
        }

        public void OnDestroy()
        {
            drawInheritance -= inheritance.DrawInheritances;
            saveNode.Inheritance = inheritance;
            saveNode.Nodes = nodes;
            saveNode.SetInheritance = setInheritance;
            AssetDatabase.SaveAssets();
        }
    }
}