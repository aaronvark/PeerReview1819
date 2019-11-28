using System;
using System.Linq;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;

namespace WorldVariables.Editor {
public class VariableField : VisualElement {
    private const string USS_CLASS_NAME = "worldvar-field";
    private const string TYPE_CLASS_NAME_TEMPLATE = "worldvar-type-";

    private Guid guid;

    private readonly string fieldName;
    private readonly VisualElement fieldContainer;
    private readonly Label label;
    private readonly EnumField typeChoice;
    private readonly VariableCollection worldVars = VariableCollection.Instance;

    public VariableField(Guid id) {
        guid = id;
        fieldName = $"worldvar-value-field-{worldVars.GetName(guid)}";

        AddToClassList(USS_CLASS_NAME);

        label = new Label(worldVars.GetName(guid));
        Add(label);

        // Create the type changer dropdown
        typeChoice = new EnumField(worldVars.GetType(guid));
        typeChoice.AddToClassList("worldvar-type-dropdown");
        Add(typeChoice);

        // Register type update callback on dropdown value change
        typeChoice.RegisterCallback<ChangeEvent<Enum>>(UpdateWorldvarType);

        // Add a type-specific class based on the current type
        AddToClassList(TYPE_CLASS_NAME_TEMPLATE + typeChoice.text.ToLower());

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
        // Method to actually remove it is the Remove() method so link that
        button.clickable.clicked += Remove;
        Add(button);

        // Register to collection change events
        worldVars.VariableRemoved += (guid) => {
            if (guid == id) RemoveFromHierarchy();
        };

        worldVars.GetVariable(guid).ValueChanged += SetFieldValue;
        worldVars.GetVariable(guid).NameChanged += SetLabelText;
        worldVars.GetVariable(guid).TypeChanged += OnTypeChange;
    }

    private void DrawValueField() {
        // Clear previous field
        if (fieldContainer.Children().Any()) {
            fieldContainer.Clear();
        }

        // Create a type-specific field for each different variable type, set the right value (mainly null handling),
        // and register the type-specific callback
        VisualElement field;
        switch (worldVars.GetType(guid)) {
            case VariableType.String:
                field = new TextField();
                field.RegisterCallback<ChangeEvent<string>>(UpdateWorldvarValue);
                break;
            case VariableType.Bool:
                field = new Toggle();
                field.RegisterCallback<ChangeEvent<bool>>(UpdateWorldvarValue);
                break;
            case VariableType.Long:
                field = new LongField();
                field.RegisterCallback<ChangeEvent<long>>(UpdateWorldvarValue);
                break;
            case VariableType.Double:
                field = new DoubleField();
                field.RegisterCallback<ChangeEvent<double>>(UpdateWorldvarValue);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        field.name = fieldName;
        fieldContainer.Add(field);

        SetFieldValue(worldVars.GetValue(guid));
    }

    private void SetFieldValue(object value) {
        switch (worldVars.GetType(guid)) {
            case VariableType.String:
                // Set the contents of the field to the world var, or to an empty string if the world var is null
                var tField = fieldContainer.Q<TextField>();
                if (tField.value != (string) value) {
                    tField.value = (string) value ?? string.Empty;
                }

                break;
            case VariableType.Bool:
                // Set the state of the toggle button to reflect the world var, or false if the world var is null
                var toggle = fieldContainer.Q<Toggle>();
                if (toggle.value != (bool) value) {
                    toggle.value = (bool?) value ?? false;
                }

                break;
            case VariableType.Long:
                // Set the contents of the field to reflect the world var, or 0 if the world var is null
                var lField = fieldContainer.Q<LongField>();
                if (lField.value != (long) value) {
                    lField.value = (long?) value ?? 0L;
                }

                break;
            case VariableType.Double:
                // Set the contents of the field to reflect the world var, or 0f if the world var is null
                var dField = fieldContainer.Q<DoubleField>();
                if (dField.value != (double) value) {
                    dField.value = (double?) value ?? 0d;
                }

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void SetLabelText(string text) {
        label.text = text;
    }

    private void OnTypeChange(VariableType oldType, VariableType newType) {
        if ((VariableType) typeChoice.value != newType) {
            typeChoice.value = newType;
        }

        DrawValueField();

        // Update style classes to the new type. The enum variable string is simply appended to the 
        RemoveFromClassList($"{TYPE_CLASS_NAME_TEMPLATE}{oldType.ToString().ToLower()}");
        AddToClassList($"{TYPE_CLASS_NAME_TEMPLATE}{newType.ToString().ToLower()}");
    }

    private void UpdateWorldvarValue<T>(ChangeEvent<T> evt) {
        VariableCollection.Instance.SetValue(guid, evt.newValue);
    }

    private void UpdateWorldvarType(ChangeEvent<Enum> evt) {
        VariableCollection.Instance.ChangeType(guid, (VariableType) evt.newValue);
    }

    private void Remove() {
        VariableCollection.Instance.RemoveVariable(guid);
    }
}
}
