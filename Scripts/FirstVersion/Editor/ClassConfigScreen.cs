using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ClassConfigScreen
{
    public bool ClearVariableToggle;
    public UMLNode umlNode;

    private AccessModifiers classAccessModifiers;
    private AccessModifiers variableAccessModifiers;
    private AccessModifiers functionAccessModifiers;


    private string className;
    private string inheritance = "MonoBehaviour";
    private string variableTypeTemp;
    private string variableNameTemp;

    private string returnTypeTemp;
    private string functionNameTemp;

    private ClassInformation information;
    public ClassInformation Information
    {
        get;
        set;
    }

    private bool initializeClass = false;

    public void OnGUI(Vector2 screenDimensions)
    {
        if (!initializeClass)
        {
            information = new ClassInformation();
            information.Variables = new List<string>();
            information.Functions = new List<string>();
            initializeClass = true;
        }
        GUI.Box(new Rect(0,0,screenDimensions.x,screenDimensions.y), "Create Class");
        
        //class name
        //classAccessModifiers = (AccessModifiers)EditorGUILayout.EnumPopup(classAccessModifiers);
        GUI.Label(new Rect(screenDimensions.x * 0.15f, screenDimensions.y * 0.05f, 100, 20),"Class Name");
        className = GUI.TextField(new Rect(screenDimensions.x * 0.15f, screenDimensions.y * 0.1f, 100, 20), className);

        GUI.Label(new Rect(screenDimensions.x * 0.23f, screenDimensions.y * 0.1f, 10, 20), ":");

        GUI.Label(new Rect(screenDimensions.x * 0.25f, screenDimensions.y * 0.05f, 100, 20), "Inhieritance");
        inheritance = GUI.TextField(new Rect(screenDimensions.x * 0.25f, screenDimensions.y * 0.1f, 100, 20),inheritance);

        // add variables to the class.
        //variableAccessModifiers = (AccessModifiers)EditorGUILayout.EnumPopup((AccessModifiers)variableAccessModifiers);
        GUI.Label(new Rect(screenDimensions.x * 0.15f, screenDimensions.y * 0.2f, 300, 20), "Variable Type");
        variableTypeTemp = GUI.TextField(new Rect(screenDimensions.x * 0.15f, screenDimensions.y * 0.25f, 100, 20), variableTypeTemp);
        
        //add the variable name.
        GUI.Label(new Rect(screenDimensions.x * 0.25f, screenDimensions.y * 0.2f, 300, 20), "Variable Name");
        variableNameTemp = GUI.TextField(new Rect(screenDimensions.x * 0.25f, screenDimensions.y * 0.25f, 100, 20), variableNameTemp);

        //add vaiables to the class button
        if (GUI.Button(new Rect(screenDimensions.x * 0.35f, screenDimensions.y * 0.25f, 50, 20), "Add"))
        {
            information.Variables.Add(variableTypeTemp + " " + variableNameTemp);
            variableNameTemp = string.Empty;
            variableTypeTemp = string.Empty;
        }

        // add functions to the class.
        //functionAccessModifiers = (AccessModifiers)EditorGUILayout.EnumPopup((AccessModifiers)functionAccessModifiers);
        GUI.Label(new Rect(screenDimensions.x * 0.15f, screenDimensions.y * 0.35f, 300, 20), "Return Type");
        returnTypeTemp = GUI.TextField(new Rect(screenDimensions.x * 0.15f, screenDimensions.y * 0.4f, 100, 20), returnTypeTemp);


        // add the function name
        GUI.Label(new Rect(screenDimensions.x * 0.25f, screenDimensions.y * 0.35f, 300, 20), "Function Name");
        functionNameTemp = GUI.TextField(new Rect(screenDimensions.x * 0.25f, screenDimensions.y * 0.4f, 100, 20), functionNameTemp);

        //add functions to the class button
        if (GUI.Button(new Rect(screenDimensions.x * 0.35f, screenDimensions.y * 0.4f, 50, 20), "Add"))
        { 
            information.Functions.Add(returnTypeTemp + " " + functionNameTemp);
            functionNameTemp = string.Empty;
            returnTypeTemp = string.Empty;
        }

        //cancel
        if (GUI.Button(new Rect(screenDimensions.x * 0.8f, screenDimensions.y * 0.9f, 100, 30), "Cancel"))
        {
            ClearVariableToggle = !ClearVariableToggle;
            ClearVariables();
        }

        ShowInformation(screenDimensions);
    }

    /// <summary>
    /// Showing Class information
    /// </summary>
    private void ShowInformation(Vector2 screenDimensions)
    {
        Rect boxRect = new Rect(screenDimensions.x * 0.5f, screenDimensions.y * 0.05f, screenDimensions.x * 0.5f, screenDimensions.y * 0.8f);
        GUI.Box(boxRect, "");

        List<string> functions = information.Functions;
        List<string> variables = information.Variables;

        //for (int i = 0; i < variables.Count; i++)
            

    }


    /// <summary>
    /// Use for editing values
    /// </summary>
    public void EditInformation(UMLNode umlNode, Vector2 screenDimensions)
    {
        umlNode.ClassName = className;

        for (int i = 0; i < umlNode.Functions.Count; i++)
            information.Functions.Add(umlNode.Functions[i]);
        for (int i = 0; i < umlNode.Variables.Count; i++)
            information.Variables.Add(umlNode.Variables[i]);
        initializeClass = true;

        OnGUI(screenDimensions);
    }

    public UMLNode CreateNode()
    {
        return new UMLNode(className,inheritance, information.Variables, information.Functions);
    }

    public void ClearVariables()
    {
        className = string.Empty;
        initializeClass = false;
        information = new ClassInformation();
    }
}
public struct ClassInformation
{
    private List<string> functions;
    public List<string> Functions
    {
        get
        {
            return functions;
        }
        set
        {
            functions = value;
        }
    }

    private List<string> variables;
    public List<string> Variables
    {
        get
        {
            return variables;
        }
        set
        {
            variables = value;
        }
    }
}
public enum AccessModifiers
{
    _private,
    _public,
    _protected
}
