using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public delegate void CheckEvent();

public class UMLWindow : EditorWindow
{
    private ClassGenerator generator = new ClassGenerator();
    private Dictionary<int, UMLNode> nodes = new Dictionary<int, UMLNode>();
    private ClassConfigScreen addingScriptOverlay;
    private List<UMLLine> lines = new List<UMLLine>();
    private event CheckEvent checkEvent;

    private Color defaultUnityColor;
    private Vector2 dimensions;
    private List<Vector2> clickPositions = new List<Vector2>();

    //grid variables
    private Vector2 offset;
    private Vector2 drag;

    //Initializing objects
    private bool initialise = false;

    [MenuItem("Custom Tools/UML Window #u")]
    private static void Init()
    {
        UMLWindow umlWindow = new UMLWindow();
        umlWindow.titleContent = new GUIContent("UML Creator");
        GUIContent icon = EditorGUIUtility.IconContent("animationdopesheetkeyframe");
        umlWindow.titleContent.image = icon.image;
        umlWindow.Show();
    }

    private void Initialization()
    {
        //initialise the script overlay
        addingScriptOverlay = new ClassConfigScreen();
        dimensions = new Vector2(position.width, position.height);
        initialise = true;
    }

    private void OnGUI()
    {
        BeginWindows();

        #region Drawing grid source: http://gram.gs/gramlog/creating-node-based-editor-unity/
        DrawGrid(20, 0.2f, Color.black);
        DrawGrid(100, 0.4f, Color.black);
        #endregion
        //initialize
        if (!initialise)
            Initialization();

        //Adding new script overlay
        if (addingScriptOverlay != null)
            if (addingScriptOverlay.ClearVariableToggle)
            {
                addingScriptOverlay.OnGUI(new Vector2(position.width, position.height));
                //adding node
                if (GUI.Button(new Rect(position.width * 0.9f, position.height * 0.9f, 100, 30), "Apply"))
                    AddNode();
            }
            else
            {
                //Inheritance lines;
                if (lines.Count > 0)
                    for (int i = 0; i < lines.Count; i++)
                        lines[i].OnGUI();

                //Edit Script Button
                //New Script Button
                if (GUI.Button(new Rect(position.width * 0.9f, position.height * 0.9f, 100, 30), "New Script"))
                    addingScriptOverlay.ClearVariableToggle = !addingScriptOverlay.ClearVariableToggle;

                if (GUI.Button(new Rect(position.width * 0.9f, position.height * 0.85f, 100, 30), "Generate C#"))
                    if (nodes.Count > 0)
                        foreach (KeyValuePair<int, UMLNode> item in nodes)
                            generator.GenerateNode(item.Value);

                //subscribe the checkevent to the checkmouseClicks function.
                //if (GUI.Button(new Rect(position.width * 0.81f, position.height * 0.9f, 125, 30), "New Connection"))
                //    checkEvent += CheckMouseForClicks;

                //Checking the nodes count.
                if (nodes.Count >= 0)
                    //drawing the node
                    foreach (KeyValuePair<int, UMLNode> node in nodes)
                        node.Value.OnGUI(node.Key, dimensions);

                //invoke Check event.
                //checkEvent?.Invoke();
            }

        if (GUI.changed)
            Repaint();

        EndWindows();

    }

    //Drawgrid source http://gram.gs/gramlog/creating-node-based-editor-unity/
    //Drawing the grid in the editor.
    private void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor)
    {
        int widthDivs = Mathf.CeilToInt(position.width / gridSpacing);
        int heightDivs = Mathf.CeilToInt(position.height / gridSpacing);

        Handles.BeginGUI();
        Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

        offset += drag * 0.5f;
        Vector3 newOffset = new Vector3(offset.x % gridSpacing, offset.y % gridSpacing, 0);

        for (int i = 0; i < widthDivs; i++)
            Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset, new Vector3(gridSpacing * i, position.height, 0f) + newOffset);

        for (int j = 0; j < heightDivs; j++)
            Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset, new Vector3(position.width, gridSpacing * j, 0f) + newOffset);

        Handles.color = Color.white;
        Handles.EndGUI();
    }

    //Create the node in the grid.
    private void AddNode()
    {
        UMLNode temp = addingScriptOverlay.CreateNode();
        //adding the node to collection
        nodes.Add(nodes.Count, temp);
        //whipe variables on the screen
        addingScriptOverlay.ClearVariables();

        //toggle menu
        addingScriptOverlay.ClearVariableToggle = !addingScriptOverlay.ClearVariableToggle;
    }

    private void CheckMouseForClicks()
    {
        Event e = Event.current;

        if (e.type == EventType.MouseUp)
        {
            clickPositions.Add(e.mousePosition);
            if (clickPositions.Count > 1)
            {
                checkEvent -= CheckMouseForClicks;
                UMLLine line = new UMLLine(new Vector2(clickPositions[0].x, clickPositions[0].y), new Vector2(clickPositions[1].x, clickPositions[1].y),UmlLineType.Generalization);
                lines.Add(line);
            }
        }
    }

}
