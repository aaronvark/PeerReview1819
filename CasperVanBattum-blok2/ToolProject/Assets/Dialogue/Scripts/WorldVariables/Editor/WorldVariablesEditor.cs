﻿using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace WorldVariables.Editor {
public class WorldVariablesEditor : EditorWindow {
    private VisualElement variableContainer;

    [MenuItem("Tools/World variable editor")]
    public static void ShowWindow() {
        var window = GetWindow<WorldVariablesEditor>();

        window.titleContent = new GUIContent("World variables");
    }

    public void OnEnable() {
        var root = rootVisualElement;

        // Load the styles
        var stylesheet = Resources.Load<StyleSheet>("WorldVariables_styles");
        root.styleSheets.Add(stylesheet);

        // Load the UXML
        var visualTree = Resources.Load<VisualTreeAsset>("WorldVariables_structure");
        visualTree.CloneTree(root);

        // Setup the new variable buttons
        root.Query("button-container").Children<Button>().ForEach(SetupNewVarButton);

        // Load the container where the variables are going to sit
        variableContainer = root.Q<VisualElement>("vars-container");

        LoadInitialVariables();
    }

    private void LoadInitialVariables() {
        // Alias for the world variable instance
        var worldVars = VariableCollection.Instance;

        Debug.Log($"There are {worldVars.NameList().Count()} variables curerntly presetn");

        foreach (var name in worldVars.NameList()) {
            var type = worldVars.GetType(name);

            object value;
            switch (type) {
                case VariableType.String:
                    value = worldVars.GetStringValue(name);
                    break;
                case VariableType.Bool:
                    value = worldVars.GetBoolValue(name);
                    break;
                case VariableType.Long:
                    value = worldVars.GetLongValue(name);
                    break;
                case VariableType.Double:
                    value = worldVars.GetDoubleValue(name);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            AddVarField(name, type, value);
        }
    }

    private void SetupNewVarButton(Button b) {
        switch (b.name) {
            case "add-str":
                b.clickable.clicked += () => AddVariable("New String", VariableType.String);
                break;
            case "add-bool":
                b.clickable.clicked += () => AddVariable("New Bool", VariableType.Bool);
                break;
            case "add-long":
                b.clickable.clicked += () => AddVariable("New Long", VariableType.Long);
                break;
            case "add-dbl":
                b.clickable.clicked += () => AddVariable("New Double", VariableType.Double);
                break;
            default:
                Debug.LogError("Button name not recognized while initializing variable addition buttons");
                break;
        }
    }

    private void AddVariable(string varName, VariableType type) {
        // Alias
        var worldVars = VariableCollection.Instance;

        // Try to add number to the standard name until a name is found that has not yet been taken
        var success = false;
        var count = 0;
        var name = varName;
        while (!success) {
            name = count == 0 ? varName : $"{varName} {count}";
            switch (type) {
                case VariableType.String:
                    success = worldVars.AddVariable(name, "");
                    break;
                case VariableType.Bool:
                    success = worldVars.AddVariable(name, false);
                    break;
                case VariableType.Long:
                    success = worldVars.AddVariable(name, 0);
                    break;
                case VariableType.Double:
                    success = worldVars.AddVariable(name, 0f);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            count++;
        }

        AddVarField(name, type);

        worldVars.DebugStats();
    }

    private void AddVarField(string varName, VariableType type, object value = null) {
        variableContainer.Add(new VariableField(varName, type, value));
    }
}
}
