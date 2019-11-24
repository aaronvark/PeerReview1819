using System;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace WorldVariables.Editor {
public class VariableField : VisualElement {
    private const string ussClassName = "worldvar-field";
    private const string typeClassNameTemplate = "worldvar-type-";

    private VariableType varType;
    private string varName;
    
    private VisualElement fieldContainer;

    public VariableField(string name, VariableType type) {
        varName = name;
        varType = type;

        AddToClassList(ussClassName);

        Add(new Label(name));

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

        // Create a type-specific field for each different variable type
        VisualElement field;
        switch (varType) {
            case VariableType.String:
                field = new TextField();
                break;
            case VariableType.Bool:
                field = new Toggle();
                break;
            case VariableType.Long:
                field = new LongField();
                break;
            case VariableType.Double:
                field = new DoubleField();
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
