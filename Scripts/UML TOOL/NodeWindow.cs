using System.Collections.Generic;
using UnityEditor;

namespace UnityEngine.Scripting.UML
{
    public class NodeWindow : EditorWindow
    {
        private Grid grid;
        private List<Node> nodes;
        private Inheritance inheritance;
        private int id = 0;
        public delegate void DrawInheritance();
        private event DrawInheritance drawInheritance;
        private SaveNodes saveNode;
        private Vector2 mousePosition;

        [MenuItem("Custom Tools/UML Window #u ")]
        private static void Init()
        {
            NodeWindow umlWindow = new NodeWindow();
            //umlWindow.nodes = new List<Node>();

            try
            {
                umlWindow.saveNode = (SaveNodes)AssetDatabase.LoadAssetAtPath("Assets/SaveNode.asset", typeof(SaveNodes));
                umlWindow.nodes = umlWindow.saveNode.Nodes;
                umlWindow.inheritance = umlWindow.saveNode.Inheritance;

                if (umlWindow.inheritance != null)
                    umlWindow.drawInheritance();
            } catch
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

        /// <summary>
        /// One call per event
        /// </summary>
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
            if (nodes == null)
                nodes = new List<Node>();

            if (nodes.Count >= 1)
            {
                for (int i = 0; i < nodes.Count; i++)
                    nodes[i].OnGUI();
            }


            drawInheritance?.Invoke();
            EndWindows();
        }

        /// <summary>
        /// Adding node
        /// </summary>
        private void AddNode()
        {
            id++;
            nodes.Add(new Node(id, this, mousePosition));
        }

        /// <summary>
        /// Delete node of the nodes list
        /// </summary>
        /// <param name="node"></param>
        public void DeleteNode(Node node)
        {
            nodes.Remove(node);
        }

        /// <summary>
        /// Generate Classes
        /// </summary>
        private void GenerateNodes()
        {
            ICodeGenerator cw = new ClassWriter();

            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].Instance != null)
                    cw.GenerateClass(nodes[i].NodeInfo);
            }
        }

        /// <summary>
        /// Updating the inheritance
        /// </summary>
        public void UpdateInheritance()
        {
            BeginDrawingInheritance();
        }


        /// <summary>
        /// Drawt the inheritance lines
        /// </summary>
        private void BeginDrawingInheritance()
        {
            if (nodes.Count < 2)
                return;

            List<Node> inheritanceNodes = new List<Node>();

            for (int i = 0; i < nodes.Count; i++)
                if (nodes[i].NodeInfo.Parent != string.Empty)
                    inheritanceNodes.Add(nodes[i]);

            if (inheritance == null)
                inheritance = new Inheritance();

            if (nodes.Count > 1)
                inheritance.SetInheritance(nodes);

            drawInheritance += inheritance.DrawInheritances;

        }

        /// <summary>
        /// Creating the Generic left click menu.
        /// </summary>
        public void CreateGenericMenu()
        {
            Event e = Event.current;
            mousePosition = e.mousePosition;

            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Add class"), false, AddNode);

            if (nodes.Count > 1)
                menu.AddItem(new GUIContent("Set Inheritance"), false, BeginDrawingInheritance);
            else
                menu.AddDisabledItem(new GUIContent("Set Inheritance"));

            if (nodes.Count > 0)
                menu.AddItem(new GUIContent("Generate class"), false, GenerateNodes);
            else
                menu.AddDisabledItem(new GUIContent("Generate class"));

            menu.ShowAsContext();
        }

        /// <summary>
        /// When the Window closes
        /// </summary>
        public void OnDisable()
        {
            if (drawInheritance != null)
                drawInheritance -= inheritance.DrawInheritances;

                saveNode.Inheritance = inheritance;
            if (nodes.Count > 0)
                saveNode.Nodes = nodes;

            AssetDatabase.SaveAssets();
        }
    }
}