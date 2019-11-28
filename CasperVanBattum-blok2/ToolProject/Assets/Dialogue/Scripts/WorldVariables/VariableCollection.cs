using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace WorldVariables {
public enum VariableType {
    String,
    Bool,
    Long,
    Double
}

[Serializable]
public class VariableCollection {
    [NonSerialized] private const string FILENAME = "worldvariables.dat";

    private readonly Dictionary<Guid, WorldVariable> variables = new Dictionary<Guid, WorldVariable>();

    [NonSerialized] private bool autosave;

    private static VariableCollection instance;
    public static VariableCollection Instance => instance ?? (instance = Load());

    /// <summary>
    /// When set to true, the collection will be saved to a file every time a change is made.
    /// See <see cref="CollectionChanged"/>.
    /// </summary>
    public bool Autosave {
        get => autosave;
        set {
            autosave = value;

            // The save method should be invoked every time the collection is changed while autosave is on.
            if (value) {
                CollectionChanged += Save;
            }
            else {
                CollectionChanged -= Save;
            }
        }
    }

    /// <summary>
    /// Is invoked each time a change is made to the data in this collection. The definition of change includes but
    /// isn't limited to variable addition or removal or data mutation.
    /// </summary>
    [field: NonSerialized] public event Action CollectionChanged;

    public override string ToString() {
        var msg = $"Contains {variables.Count} variables\n";
        msg = variables.Aggregate(msg,
            (current, varName) => {
                return $"{current}{varName.Key}: {varName.Value.Name} | {varName.Value.Type} | {varName.Value.Value}\n";
            });

        return msg;
    }

    public object GetValue(Guid id) {
        return variables[id].Value;
    }

    public VariableType GetType(Guid id) {
        return variables[id].Type;
    }

    public string GetName(Guid id) {
        return variables[id].Name;
    }

    public bool AddVariable(string name, string value, out Guid id) {
        return AddVariable(name, value, VariableType.String, out id);
    }

    public bool AddVariable(string name, bool value, out Guid id) {
        return AddVariable(name, value, VariableType.Bool, out id);
    }

    public bool AddVariable(string name, long value, out Guid id) {
        return AddVariable(name, value, VariableType.Long, out id);
    }

    public bool AddVariable(string name, double value, out Guid id) {
        return AddVariable(name, value, VariableType.Double, out id);
    }

    public bool AddVariable(string name, string value) {
        return AddVariable(name, value, VariableType.String, out _);
    }

    public bool AddVariable(string name, bool value) {
        return AddVariable(name, value, VariableType.Bool, out _);
    }

    public bool AddVariable(string name, long value) {
        return AddVariable(name, value, VariableType.Long, out _);
    }

    public bool AddVariable(string name, double value) {
        return AddVariable(name, value, VariableType.Double, out _);
    }

    public bool AddEmptyVariable(string name, VariableType type, out Guid id) {
        object val;
        switch (type) {
            case VariableType.String:
                val = "";
                break;
            case VariableType.Bool:
                val = false;
                break;
            case VariableType.Long:
                val = 0L;
                break;
            case VariableType.Double:
                val = 0d;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        return AddVariable(name, val, type, out id);
    }

    private bool AddVariable(string name, object value, VariableType type, out Guid id) {
        if (!NameExists(name)) {
            var newVariable = new WorldVariable(name, type, value);
            id = newVariable.Guid;
            variables.Add(newVariable.Guid, newVariable);
            CollectionChanged?.Invoke();

            return true;
        }

        return false;
    }

    public void RenameVariable(Guid id, string newName) {
        if (NameExists(newName)) {
            throw new InvalidOperationException("New name for variable already exists!");
        }

        variables[id].Name = newName;

        CollectionChanged?.Invoke();
    }

    public void RemoveVariable(Guid id) {
        if (!VariableExists(id)) {
            throw new InvalidOperationException("Tried to remove a non-existant variable");
        }

        variables.Remove(id);
        CollectionChanged?.Invoke();
    }

    public void ChangeType(Guid id, VariableType newType) {
        if (!VariableExists(id)) {
            throw new InvalidOperationException("Tried to change type of a non-existant variable");
        }

        var newValue = ConvertValue(variables[id].Value, variables[id].Type, newType);
        variables[id].Type = newType;
        variables[id].Value = newValue;

        // Invoke change event
        CollectionChanged?.Invoke();
    }

    public void SetValue(Guid id, object newValue) {
        // Check if a variable with the given name exists
        if (!VariableExists(id)) {
            throw new InvalidOperationException($"Unknown variable id: {id}");
        }

        // Check if type of the new value matches original type registered in the collection
        var originalType = variables[id].Type;
        var name = variables[id].Name;
        switch (newValue) {
            case string _:
                if (originalType != VariableType.String) {
                    throw new InvalidOperationException(
                        $"{name} is a string, but the new value was of type {newValue.GetType()}");
                }

                break;
            case bool _:
                if (originalType != VariableType.Bool) {
                    throw new InvalidOperationException(
                        $"{name} is a bool, but the new value was of type {newValue.GetType()}");
                }

                break;
            case long _:
                if (originalType != VariableType.Long) {
                    throw new InvalidOperationException(
                        $"{name} is a long, but the new value was of type {newValue.GetType()}");
                }

                break;
            case double _:
                if (originalType != VariableType.Double) {
                    throw new InvalidOperationException(
                        $"{name} is a double, but the new value was of type {newValue.GetType()}");
                }

                break;
            default:
                throw new InvalidOperationException(
                    $"Tried to assign value of type {newValue.GetType()} to variable of type {variables[id].Type}");
        }

        variables[id].Value = newValue;

        CollectionChanged?.Invoke();
    }

    public IEnumerable<Guid> VariableList() {
        return variables.Keys;
    }

    private bool NameExists(string name) {
        return variables.Keys.Any(key => variables[key].Name == name);
    }

    public bool VariableExists(Guid id) {
        return variables.ContainsKey(id);
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

    #region SAVING

    /// <summary>
    /// Save this collection to the StreamingAssets
    /// </summary>
    public void Save() {
        var formatter = new BinaryFormatter();
        var path = Path.Combine(Application.streamingAssetsPath, FILENAME);

        using (var stream = new FileStream(path, FileMode.Create)) {
            formatter.Serialize(stream, this);
        }
    }

    /// <summary>
    /// Load the collection from the StreamingAssets folder. If no file exists, a new instance is returned.
    /// </summary>
    private static VariableCollection Load() {
        var formatter = new BinaryFormatter();
        var path = Path.Combine(Application.streamingAssetsPath, FILENAME);

        // If the file doesn't exist yet, a new collection should be started. Save this to the path immediately.
        if (!File.Exists(path)) {
            var newCollection = new VariableCollection();
            newCollection.Save();
            return newCollection;
        }

        VariableCollection collection = null;
        using (var stream = new FileStream(path, FileMode.Open)) {
            collection = (VariableCollection) formatter.Deserialize(stream);
        }

        return collection;
    }

    #endregion
}
}
