using System;
using System.Linq;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;

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

        // Create a button at the end to remove the var
        var button = new Button() {
            text = "-",
            name = $"remove-{varName}"
        };
        button.AddToClassList("worldvar-remove-button");
        button.clickable.clicked += Remove;
        Add(button);
    }

    private void DrawValueField() {
        var fieldName = $"worldvar-value-field-{varName}";

        // Clear previous field
        if (fieldContainer.Children().Any()) {
            fieldContainer.Clear();
        }

        // Create a type-specific field for each different variable type
        VisualElement field;
        switch (varType) {
            case VariableType.String:
                field = new TextField() {
                    value = value?.ToString()
                };
                field.RegisterCallback((ChangeEvent<string> evt) => {
//                    Debug.Log($"Fired from string. name: {varName} {evt.newValue}");
                    VariableCollection.Instance.SetValue(varName, evt.newValue);
                    VariableCollection.Instance.DebugDump();
                });
                break;
            case VariableType.Bool:
                field = new Toggle {
                    value = value != null && bool.Parse(value?.ToString())
                };
                field.RegisterCallback((ChangeEvent<bool> evt) => {
//                    Debug.Log($"Fired from bool. name: {varName} {evt.newValue}");
                    VariableCollection.Instance.SetValue(varName, evt.newValue);
                    VariableCollection.Instance.DebugDump();
                });
                break;
            case VariableType.Long:
                field = new LongField() {
                    value = value != null ? long.Parse(value?.ToString()) : 0
                };
                field.RegisterCallback((ChangeEvent<long> evt) => {
//                    Debug.Log($"Fired from long. name: {varName} {evt.newValue}");
                    VariableCollection.Instance.SetValue(varName, evt.newValue);
                    VariableCollection.Instance.DebugDump();
                });
                break;
            case VariableType.Double:
                field = new DoubleField {
                    value = value != null ? double.Parse(value?.ToString()) : 0
                };
                field.RegisterCallback((ChangeEvent<double> evt) => {
//                    Debug.Log($"Fired from double. name: {varName} {evt.newValue}");
                    VariableCollection.Instance.SetValue(varName, evt.newValue);
                    VariableCollection.Instance.DebugDump();
                });
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        field.name = fieldName;
        fieldContainer.Add(field);
    }

    private void UpdateVariableType(ChangeEvent<Enum> evt) {
        // Update the type to the new value and redraw the value field
        value = null;
        varType = (VariableType) evt.newValue;
        DrawValueField();

        // Update style classes to the new type. The enum variable string is simply appended to the 
        RemoveFromClassList($"{typeClassNameTemplate}{evt.previousValue.ToString().ToLower()}");
        AddToClassList($"{typeClassNameTemplate}{evt.newValue.ToString().ToLower()}");
    }

//    /// <summary>
//    /// Handles special conversions in case it's not possible to simply parse these values
//    /// </summary>
//    private void CarryValue(VariableType oldType, VariableType newType) {
//        if (oldType == newType) return;
//        if (newType == VariableType.String) return;
//
//        // Bool to numeric conversions or vice versa always reset to 0
//        if (oldType == VariableType.Bool && newType != VariableType.String ||
//            oldType != VariableType.String && newType == VariableType.Bool) {
//            value = null;
//            return;
//        }
//
//        // String to bool can only be converted if the string parses to a bool, otherwise simply reset to false
//        if (oldType == VariableType.String) {
//            if (newType == VariableType.Bool) {
//                if (!bool.TryParse(value.ToString(), out _)) {
//                    value = false;
//                    return;
//                }
//            }
//            else if (newType == VariableType.Long) {
//                if (! long.TryParse(value.ToString(), out _))
//                value = 0L;
//            }
//            else if (newType == VariableType.Double) value = 0d;
//            return;
//        }
//
//        if (oldType == VariableType.Double && newType == VariableType.Long) {
//            value = Convert.ToInt64((double) value);
//            return;
//        }
//
//        if (oldType == VariableType.Long && newType == VariableType.Double) {
//            value = Convert.ToDouble((long) value);
//        }
//    }

    private void Remove() {
        VariableCollection.Instance.RemoveVariable(varName);
        RemoveFromHierarchy();

        VariableCollection.Instance.DebugDump();
    }
}
}
