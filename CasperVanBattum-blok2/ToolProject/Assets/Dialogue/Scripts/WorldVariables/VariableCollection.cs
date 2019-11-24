using System;
using System.Collections.Generic;

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

    public void AddVariable(string name, string value) {
        if (AddName(name, VariableType.String)) stringVariables.Add(name, value);
    }

    public void AddVariable(string name, bool value) {
        if (AddName(name, VariableType.Bool)) boolVariables.Add(name, value);
    }

    public void AddVariable(string name, long value) {
        if (AddName(name, VariableType.Long)) longVariables.Add(name, value);
    }

    public void AddVariable(string name, double value) {
        if (AddName(name, VariableType.Double)) doubleVariables.Add(name, value);
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

    public IEnumerable<string> NameList() {
        return names.Keys;
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
