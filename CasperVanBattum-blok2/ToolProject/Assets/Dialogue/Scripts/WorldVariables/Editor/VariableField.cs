using System;
using System.Linq;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;

namespace WorldVariables.Editor {
public class VariableField : VisualElement {
    private const string USS_CLASS_NAME = "worldvar-field";
    private const string TYPE_CLASS_NAME_TEMPLATE = "worldvar-type-";

//    private VariableType varType;
//    private string varName;
    private Guid guid;
//    private object value;

    private readonly VisualElement fieldContainer;
    private readonly VariableCollection worldVars = VariableCollection.Instance;

    public VariableField(Guid id) {
        guid = id;

        AddToClassList(USS_CLASS_NAME);

        var label = new Label(worldVars.GetName(guid));
        Add(label);

        // Create the type changer dropdown
        var choice = new EnumField(worldVars.GetType(guid));
        choice.AddToClassList("worldvar-type-dropdown");
        Add(choice);

        // Register type update callback on dropdown value change
        choice.RegisterCallback<ChangeEvent<Enum>>(UpdateWorldvarType);

        // Add a type-specific class based on the current type
        AddToClassList(TYPE_CLASS_NAME_TEMPLATE + choice.text.ToLower());

        // Create a simple container to hold the value field
        fieldContainer = new VisualElement();
        fieldContainer.AddToClassList("worldvar-value-container");
        Add(fieldContainer);
        DrawValueField();

        // Create a button at the end to remove the variable
        var button = new Button() {
            text = "-",
            name = $"remove-{worldVars.GetName(guid)}"
        };
        button.AddToClassList("worldvar-remove-button");
        // Method to actually remove it is the Remove() method
        button.clickable.clicked += Remove;
        Add(button);
    }

    private void DrawValueField() {
        var fieldName = $"worldvar-value-field-{worldVars.GetName(guid)}";

        // Clear previous field
        if (fieldContainer.Children().Any()) {
            fieldContainer.Clear();
        }

        // Update value to match world variable
        var value = worldVars.GetValue(guid);

        // Create a type-specific field for each different variable type, set the right value (mainly null handling),
        // and register the type-specific callback
        VisualElement field;
        switch (worldVars.GetType(guid)) {
            case VariableType.String:
                field = new TextField() {
                    // Set the contents of the field to the world var, or to an empty string if the world var is null
                    value = (string) value ?? string.Empty
                };
                field.RegisterCallback<ChangeEvent<string>>(UpdateWorldvarValue);
                break;
            case VariableType.Bool:
                field = new Toggle {
                    // Set the state of the toggle button to reflect the world var, or false if the world var is null
                    value = (bool?) value ?? false
                };
                field.RegisterCallback<ChangeEvent<bool>>(UpdateWorldvarValue);
                break;
            case VariableType.Long:
                field = new LongField() {
                    // Set the contents of the field to reflect the world var, or 0 if the world var is null
                    value = (long?) value ?? 0
                };
                field.RegisterCallback<ChangeEvent<long>>(UpdateWorldvarValue);
                break;
            case VariableType.Double:
                field = new DoubleField() {
                    // Set the contents of the field to reflect the world var, or 0f if the world var is null
                    value = (double?) value ?? 0f
                };
                field.RegisterCallback<ChangeEvent<double>>(UpdateWorldvarValue);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        field.name = fieldName;
        fieldContainer.Add(field);
    }

    private void UpdateWorldvarValue<T>(ChangeEvent<T> evt) {
        VariableCollection.Instance.SetValue(guid, evt.newValue);
    }

    private void UpdateWorldvarType(ChangeEvent<Enum> evt) {
        // Update the type to the new value and redraw the value field
        VariableCollection.Instance.ChangeType(guid, (VariableType) evt.newValue);
        DrawValueField();

        // Update style classes to the new type. The enum variable string is simply appended to the 
        RemoveFromClassList($"{TYPE_CLASS_NAME_TEMPLATE}{evt.previousValue.ToString().ToLower()}");
        AddToClassList($"{TYPE_CLASS_NAME_TEMPLATE}{evt.newValue.ToString().ToLower()}");
    }

    private void Remove() {
        VariableCollection.Instance.RemoveVariable(guid);
        RemoveFromHierarchy();
    }
}
}
