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

        foreach (var name in worldVars.NameList()) {
            var type = worldVars.GetType(name);
            var value = worldVars.GetValue(name);

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

        var varAdded = false;
        var count = 0;
        var nameCopy = varName;
        while (!varAdded) {
            // Try to add number to the standard name until a name is found that has not yet been taken
            nameCopy = count == 0 ? varName : $"{varName} {count}";
            count++;

            varAdded = worldVars.AddEmptyVariable(nameCopy, type);
        }

        AddVarField(nameCopy, type);

//        worldVars.DebugDump();
    }

    private void AddVarField(string varName, VariableType type, object value = null) {
        variableContainer.Add(new VariableField(varName, type, value));
    }

    private void OnDisable() {
        VariableCollection.Instance.Save();
    }

    private void OnLostFocus() {
        VariableCollection.Instance.Save();
    }
}
}
