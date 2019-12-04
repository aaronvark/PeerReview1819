using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UMLLine
{
    private Vector2 begin;
    private Vector2 end;
    private UmlLineType line;
    public UMLLine(Vector2 begin, Vector2 end, UmlLineType line)
    {
        this.begin = begin;
        this.end = end;
        this.line = line;
    }

    public void OnGUI()
    {
        Handles.BeginGUI();
        //set line color
        SetLineColor();

        Handles.DrawLine(begin, end);
        Handles.EndGUI();
    }

    private void SetLineColor()
    {
        switch (line)
        {
            case UmlLineType.Generalization:
                Handles.color = Color.red;
                break;
            case UmlLineType.Association:
                Handles.color = Color.cyan;
                break;
            case UmlLineType.Agggregation:
                Handles.color = Color.green;
                break;
            case UmlLineType.Dependency:
                Handles.color = Color.black;
                break;
            case UmlLineType.Composition:
                Handles.color = Color.blue;
                break;
        }
    }

    public void UpdateLinePosition(Vector2 updatedPosition, bool isBeginPosition)
    {
        if (isBeginPosition)
            this.begin = updatedPosition;
        else
            this.end = updatedPosition;
    }
}
public enum UmlLineType
{
    Generalization,
    Association,
    Agggregation,
    Dependency,
    Composition
}
