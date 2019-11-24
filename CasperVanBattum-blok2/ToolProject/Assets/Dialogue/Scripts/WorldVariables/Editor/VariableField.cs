using System;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace WorldVariables.Editor {
public class VariableField : VisualElement {
    private const string ussClassName = "worldvar-field";
    private const string typeClassNameTemplate = "worldvar-type-";

    private VariableType varType;
    private string varName;
    private object value;

    private VisualElement fieldContainer;

    public VariableField(string name, VariableType type, object value = null) {
        varName = name;
        varType = type;
        this.value = value;

        AddToClassList(ussClassName);

        var label = new Label(name);
        Add(label);

        // Create the type changer dropdown
        var choice = new EnumField(type);
        choice.AddToClassList("worldvar-type-dropdown");
        Add(choice);

        // Register type update callback on dropdown value change
        choice.RegisterCallback<ChangeEvent<Enum>>(UpdateVariableType);

        // Add a type-specific class based on the current type
        AddToClassList(typeClassNameTemplate + choice.text.ToLower());

        // Create a simple container to hold the value field
        fieldContainer = new VisualElement();
        fieldContainer.AddToClassList("worldvar-value-container");
        Add(fieldContainer);
        DrawValueField();
    }

    private void DrawValueField() {
        const string fieldName = "worldvar-value-field";

        // TODO implement value carrying
        // Clear previous field
        fieldContainer.Clear();
        value = null;

        // Create a type-specific field for each different variable type
        VisualElement field;
        switch (varType) {
            case VariableType.String:
                field = new TextField() {
                    value = value?.ToString()
                };
                break;
            case VariableType.Bool:
                field = new Toggle {
                    value = value != null && bool.Parse(value?.ToString())
                };
                break;
            case VariableType.Long:
                field = new LongField() {
                    value = value != null ? long.Parse(value?.ToString()) : 0
                };
                break;
            case VariableType.Double:
                field = new DoubleField {
                    value = value != null ? double.Parse(value?.ToString()) : 0
                };
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        field.name = fieldName;
        fieldContainer.Add(field);
    }

    private void UpdateVariableType(ChangeEvent<Enum> evt) {
        // Update the type to the new value and redraw the value field
        varType = (VariableType) evt.newValue;
        DrawValueField();

        // Update style classes to the new type. The enum variable string is simply appended to the 
        RemoveFromClassList($"{typeClassNameTemplate}{evt.previousValue.ToString().ToLower()}");
        AddToClassList($"{typeClassNameTemplate}{evt.newValue.ToString().ToLower()}");
    }
}
}
