using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UMLNode
{
    //for inheritance
    private string parent = "MonoBehaviour";
    public string Parent
    {
        get
        {
            return parent;
        }
        set
        {
            parent = value;
        }
    }

    public string ClassName;
    public List<string> Variables;
    public List<string> Functions;

    private Rect windowRect;
    private Rect nodeRect = new Rect(0.3f, 0.25f, 200, 60);

    private int windowID;
    private Vector2 scrollPos;
    private bool initialised = false;
    private bool isDragging;

    private bool deleted = false;
    public bool Deleted
    {
        get
        {
            return deleted;
        }

        set
        {
            deleted = value;
        }
    }
    public UMLNode(string className, string parent, List<string> variables, List<string> functions)
    {
        this.ClassName = className;
        this.Functions = functions;
        this.Variables = variables;
        this.parent = parent;
    }

    private void Initialise(Vector2 dimensions)
    {
        windowRect = new Rect(0, 0, dimensions.x, dimensions.y);
        initialised = true;
    }

    public void OnGUI(int id, Vector2 dimensions)
    {
        if (!initialised)
            Initialise(dimensions);

        nodeRect = GUI.Window(id, nodeRect, DragWindow, ClassName + ":" + parent);

        Event e = Event.current;
        if (nodeRect.Contains(e.mousePosition))
        {
            Debug.Log("Dragging");
            if (GUI.Button(new Rect(dimensions.x * 0.83f, dimensions.y * 0.908f, 100, 30), "Edit Script"))
                Debug.Log("Edit");
        }

    }

    private void DragWindow(int id)
    {

        //is the window Selected.
        GUI.DragWindow();
    }
}
