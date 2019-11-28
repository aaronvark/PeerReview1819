namespace UnityEngine.Scripting.UML
{
    public class Node
    {
        public Vector2 ScrollPosition = Vector2.zero;
        public Rect MinNodeSize = new Rect(30, 30, 175, 40);
        public Rect MaxNodeSize = new Rect(30, 30, 225, 250);
        public NodeInfo NodeInfo;
        public int Id;
        public bool Minalized = false;
        public Node Instance;
        private NodeWindow Window;

        public Node(int id, NodeWindow window, Vector2 mousePosition)
        {
            this.Id = id;
            this.Window = window;
            MinNodeSize.position = mousePosition;
            MaxNodeSize.position = mousePosition;
            NodeInfo = new NodeInfo(); //new NodeInfo(this);
            Instance = this;
        }

        public void OnGUI()
        {
            if (Instance != null)
            {
                Event e = Event.current;

                if (!Minalized)
                    MaxNodeSize = GUI.Window(Id, MaxNodeSize, DragNode, NodeInfo.ClassName + " node");
                else
                    MinNodeSize = GUI.Window(Id, MinNodeSize, DragNode, NodeInfo.ClassName + " node");
            }
        }

        public void DragNode(int id)
        {
            if (!Minalized)
                ScrollPosition = GUILayout.BeginScrollView(ScrollPosition, false, false);
         
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();

            if (!Minalized)
                if (GUILayout.Button("X", GUILayout.Width(20)))
                {
                    Instance = null;
                    Window.UpdateInheritance();
                    Window.DeleteNode(this);
                }

            if (Minalized)
            {
                GUILayout.Label(NodeInfo.ClassName);
                if (GUILayout.Button("+", GUILayout.MaxWidth(20)))
                    Minalized = !Minalized;
            }
            else
            {
                if (GUILayout.Button("-", GUILayout.MaxWidth(20)))
                    Minalized = !Minalized;
            }
            GUILayout.EndHorizontal();


            if (!Minalized)
                NodeInfo.Draw();
            GUILayout.EndVertical();
            if (!Minalized)
                GUILayout.EndScrollView();
            if (!Minalized)
                MinNodeSize.position = new Vector2(MaxNodeSize.x, MaxNodeSize.y);
            else
                MaxNodeSize.position = new Vector2(MinNodeSize.x, MinNodeSize.y);

            GUI.DragWindow();
            
        }
    }
}