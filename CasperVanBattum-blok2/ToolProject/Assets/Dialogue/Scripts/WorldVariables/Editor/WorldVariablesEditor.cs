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
    }

    private void SetupNewVarButton(Button b) {
        switch (b.name) {
            case "add-str":
                b.clickable.clicked += () => AddVarField("New String", VariableType.String);
                break;
            case "add-bool":
                b.clickable.clicked += () => AddVarField("New Bool", VariableType.Bool);
                break;
            case "add-long":
                b.clickable.clicked += () => AddVarField("New Long", VariableType.Long);
                break;
            case "add-dbl":
                b.clickable.clicked += () => AddVarField("New Double", VariableType.Double);
                break;
            default:
                Debug.LogError("Button name not recognized while initializing variable addition buttons");
                break;
        }
    }

    private void AddVarField(string varName, VariableType type) {
        variableContainer.Add(new VariableField(varName, type));
    }
}
}
