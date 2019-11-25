using System;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;

namespace WorldVariables {
public enum VariableType {
    String,
    Bool,
    Long,
    Double
}

public class VariableCollection {
    private static VariableCollection instance;

    public static VariableCollection Instance {
        get {
            if (instance == null) {
                instance = new VariableCollection();
                // FIXME TEST CODE PLEASE REMOVE
                instance.AddVariable("Variable1", "Some text");
                instance.AddVariable("A long", 40);
                instance.AddVariable("Test bool", true);
                instance.AddVariable("Other test bool", false);
                instance.AddVariable("moar string", "Value");
                instance.AddVariable("lmoa", 42.42);
            }

            return instance;
        }
    }

    private readonly Dictionary<string, VariableType> names = new Dictionary<string, VariableType>();
    private readonly Dictionary<string, string> stringVariables = new Dictionary<string, string>();
    private readonly Dictionary<string, bool> boolVariables = new Dictionary<string, bool>();
    private readonly Dictionary<string, long> longVariables = new Dictionary<string, long>();
    private readonly Dictionary<string, double> doubleVariables = new Dictionary<string, double>();

    public string GetStringValue(string name) {
        stringVariables.TryGetValue(name, out var value);
        return value;
    }

    public bool GetBoolValue(string name) {
        boolVariables.TryGetValue(name, out var value);
        return value;
    }

    public long GetLongValue(string name) {
        longVariables.TryGetValue(name, out var value);
        return value;
    }

    public double GetDoubleValue(string name) {
        doubleVariables.TryGetValue(name, out var value);
        return value;
    }

    public object GetValueRaw(string name) {
        switch (names[name]) {
            case VariableType.Bool:
                return boolVariables[name];
            case VariableType.Double:
                return doubleVariables[name];
            case VariableType.Long:
                return longVariables[name];
            case VariableType.String:
                return stringVariables[name];
        }

        return null;
    }

    public VariableType GetType(string name) {
        names.TryGetValue(name, out var type);
        return type;
    }

    public bool AddVariable(string name, string value) {
        if (AddName(name, VariableType.String)) {
            stringVariables.Add(name, value);
            return true;
        }

        return false;
    }

    public bool AddVariable(string name, bool value) {
        if (AddName(name, VariableType.Bool)) {
            boolVariables.Add(name, value);
            return true;
        }

        return false;
    }

    public bool AddVariable(string name, long value) {
        if (AddName(name, VariableType.Long)) {
            longVariables.Add(name, value);
            return true;
        }

        return false;
    }

    public bool AddVariable(string name, double value) {
        if (AddName(name, VariableType.Double)) {
            doubleVariables.Add(name, value);
            return true;
        }

        return false;
    }

    public void RenameVariable(string oldName, string newName) {
        if (!NameExists(oldName)) throw new InvalidOperationException("Tried to rename a non-existant variable");

        var type = names[oldName];
        names.Remove(oldName);
        names.Add(newName, type);

        // Change value list
        switch (type) {
            case VariableType.String:
            {
                var val = stringVariables[oldName];
                stringVariables.Remove(oldName);
                stringVariables.Add(newName, val);
                break;
            }
            case VariableType.Bool:
            {
                var val = boolVariables[oldName];
                boolVariables.Remove(oldName);
                boolVariables.Add(newName, val);
                break;
            }
            case VariableType.Long:
            {
                var val = longVariables[oldName];
                longVariables.Remove(oldName);
                longVariables.Add(newName, val);
                break;
            }
            case VariableType.Double:
            {
                var val = doubleVariables[oldName];
                doubleVariables.Remove(oldName);
                doubleVariables.Add(newName, val);
                break;
            }
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void RemoveVariable(string name) {
        if (!NameExists(name)) throw new InvalidOperationException("Tried to remove a non-existant variable");

        // Check which type the variable is of and remove it from that list
        switch (names[name]) {
            case VariableType.String:
            {
                stringVariables.Remove(name);
                break;
            }
            case VariableType.Bool:
            {
                boolVariables.Remove(name);
                break;
            }
            case VariableType.Long:
            {
                longVariables.Remove(name);
                break;
            }
            case VariableType.Double:
            {
                doubleVariables.Remove(name);
                break;
            }
            default:
                throw new ArgumentOutOfRangeException();
        }

        // Remove the name registry
        names.Remove(name);
    }

    // TODO change type
    public void ChangeType(string name, VariableType newType) {
        // Remove the old variable, save the value for carrying
        
        
        // Try to convert the value to the new type
        
        // Add the same variable with the updated type
    }

    public void SetValue(string name, string newValue) {
        if (!stringVariables.ContainsKey(name))
            throw new InvalidOperationException($"Unknown name for type string: {name}");

        stringVariables[name] = newValue;
    }

    public void SetValue(string name, bool newValue) {
        if (!boolVariables.ContainsKey(name))
            throw new InvalidOperationException($"Unknown name for type bool: {name}");

        boolVariables[name] = newValue;
    }

    public void SetValue(string name, long newValue) {
        if (!longVariables.ContainsKey(name))
            throw new InvalidOperationException($"Unknown name for type long: {name}");

        longVariables[name] = newValue;
    }

    public void SetValue(string name, double newValue) {
        if (!doubleVariables.ContainsKey(name))
            throw new InvalidOperationException($"Unknown name for type double: {name}");

        doubleVariables[name] = newValue;
    }

    public IEnumerable<string> NameList() {
        return names.Keys;
    }

    public void DebugDump() {
        var msg = $"Contains {names.Count} variables\n";
        foreach (var varName in names) {
            msg = $"{msg}{varName.Key} | {varName.Value} | {GetValueRaw(varName.Key)}\n";
        }

        Debug.Log(msg);
    }

    private bool NameExists(string name) {
        return names.ContainsKey(name);
    }

    private bool AddName(string name, VariableType type) {
        if (NameExists(name))
            return false;

        names.Add(name, type);
        return true;
    }
}
}
