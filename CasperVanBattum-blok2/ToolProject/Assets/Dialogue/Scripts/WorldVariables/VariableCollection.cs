using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using UnityEngine;
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

    private readonly Dictionary<string, (VariableType varType, object value)> variables =
        new Dictionary<string, (VariableType, object)>();

    public object GetValue(string name) {
        return variables[name].value;
    }

    public VariableType GetType(string name) {
        return variables[name].varType;
    }

    public bool AddVariable(string name, string value) {
        return AddVariable(name, value, VariableType.String);
    }

    public bool AddVariable(string name, bool value) {
        return AddVariable(name, value, VariableType.Bool);
    }

    public bool AddVariable(string name, long value) {
        return AddVariable(name, value, VariableType.Long);
    }

    public bool AddVariable(string name, double value) {
        return AddVariable(name, value, VariableType.Double);
    }

    private bool AddVariable(string name, object value, VariableType type) {
        if (!NameExists(name)) {
            variables.Add(name, (type, value));
            return true;
        }

        return false;
    }

    public void RenameVariable(string oldName, string newName) {
        if (!NameExists(oldName)) {
            throw new InvalidOperationException("Tried to rename a non-existant variable");
        }

        var (varType, value) = variables[oldName];
        AddVariable(newName, value, varType);
    }

    public void RemoveVariable(string name) {
        if (!NameExists(name)) {
            throw new InvalidOperationException("Tried to remove a non-existant variable");
        }

        variables.Remove(name);
    }

    public void ChangeType(string name, VariableType newType) {
        if (!NameExists(name)) {
            throw new InvalidOperationException("Tried to change type of a non-existant variable");
        }

        // Remove the old variable, save the original value and type for carrying
        var oldValue = GetValue(name);
        var oldType = GetType(name);
        RemoveVariable(name);

        // Try to convert the value to the new type
        var newValue = ConvertValue(oldValue, oldType, newType);

        // Add the same variable with the updated type
        AddVariable(name, newValue, newType);
    }

    public void SetValue(string name, object newValue) {
        // Check if a variable with the given name exists
        if (!NameExists(name)) {
            throw new InvalidOperationException($"Unknown variable name: {name}");
        }

        // Check if type of the new value matches original type registered in the collection
        var originalType = variables[name].varType;
        switch (newValue) {
            case string val:
                if (originalType != VariableType.String) {
                    throw new InvalidOperationException(
                        $"{name} is a string, but the new value was of type {newValue.GetType()}");
                }

                break;
            case bool val:
                if (originalType != VariableType.Bool) {
                    throw new InvalidOperationException(
                        $"{name} is a bool, but the new value was of type {newValue.GetType()}");
                }

                break;
            case long val:
                if (originalType != VariableType.Long) {
                    throw new InvalidOperationException(
                        $"{name} is a long, but the new value was of type {newValue.GetType()}");
                }

                break;
            case double val:
                if (originalType != VariableType.Double) {
                    throw new InvalidOperationException(
                        $"{name} is a double, but the new value was of type {newValue.GetType()}");
                }

                break;
            default:
                throw new InvalidOperationException(
                    $"Tried to assign value of type {newValue.GetType()} to variable of type {variables[name].varType}");
        }

        var data = variables[name];
        data.value = newValue;
        variables[name] = data;
    }

    public IEnumerable<string> NameList() {
        return variables.Keys;
    }

    public void DebugDump() {
        var msg = $"Contains {variables.Count} variables\n";
        msg = variables.Aggregate(msg,
            (current, varName) => {
                return $"{current}{varName.Key} | {varName.Value.varType} | {varName.Value.value}\n";
            });

        Debug.Log(msg);
    }

    private bool NameExists(string name) {
        return variables.ContainsKey(name);
    }

    private static object ConvertValue(object value, VariableType oldType, VariableType newType) {
        // Any type to string -> value.tostring()
        if (newType == VariableType.String) {
            return value.ToString();
        }

        try {
            switch (oldType) {
                //### String to any type
                // string to bool -> parse to bool
                case VariableType.String when newType == VariableType.Bool:
                {
                    // Simplified boolean expression for "TryParse(value, out res) ? res : false"
                    return bool.TryParse((string) value, out var res) && res;
                }
                // string to long -> parse to long 
                case VariableType.String when newType == VariableType.Long:
                {
                    return long.TryParse((string) value, out var res) ? res : 0L;
                }
                // string to double -> parse to double 
                case VariableType.String when newType == VariableType.Double:
                {
                    return double.TryParse((string) value, out var res) ? res : 0d;
                }
                //### Numeric to numeric
                // long to double -> cast to double
                case VariableType.Long when newType == VariableType.Double:
                    return Convert.ToDouble((long) value);
                // double to long -> round and convert to long
                case VariableType.Double when newType == VariableType.Long:
                    return (long) Math.Round((double) value);
                //### Numeric to bool
                // long/double to bool -> check if larger than 0
                case VariableType.Long when newType == VariableType.Bool:
                    return (long) value > 0;
                case VariableType.Double when newType == VariableType.Bool:
                    return (double) value > 0;
                // bool to long -> interpret the bool as a long so that false becomes 0 and true becomes 1
                case VariableType.Bool when newType == VariableType.Long:
                    return (bool) value ? 1L : 0L;
                // bool to double -> false becomes 0 and true becomes 1
                case VariableType.Bool when newType == VariableType.Double:
                    return (bool) value ? 1d : 0d;

                default:
                    return null;
            }
        }
        catch (InvalidCastException e) {
            throw new InvalidCastException(
                $"Failed to convert from {oldType} to {newType}. Value was {value} ({value.GetType()})", e);
        }
    }
}
}
