using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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

    [field: NonSerialized] public event Action<Guid> VariableAdded;

    [field: NonSerialized] public event Action<Guid> VariableRemoved;

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

    public WorldVariable GetVariable(Guid id) {
        return variables[id];
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
            VariableAdded?.Invoke(id);

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
        VariableRemoved?.Invoke(id);
    }

    public void ChangeType(Guid id, VariableType newType) {
        if (!VariableExists(id)) {
            throw new InvalidOperationException("Tried to change type of a non-existant variable");
        }

        variables[id].Type = newType;

        // Invoke change event
        CollectionChanged?.Invoke();
    }

    public void SetValue(string name, object newValue) {
        SetValue(GetGuidFromName(name), newValue);
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

    public IEnumerable<Guid> OfType(params VariableType[] types) {
//        return variables.Where(pair => types.Contains(pair.Value.Type) ? pair.Key : default);
        var ids = variables.Where(pair => types.Contains(pair.Value.Type));
        return ids.Select(pair => pair.Key);
    }
    
    public Guid GetGuidFromName(string name) {
        // Finds the guid of the variable that matches the requested name
        try {
            return variables.Single(pair => pair.Value.Name == name).Value.Guid;
        }
        catch (InvalidOperationException e) {
            throw new InvalidOperationException($"No element with name \"{name}\" exists");
        }
    }

    private bool NameExists(string name) {
        return variables.Keys.Any(key => variables[key].Name == name);
    }

    public bool VariableExists(Guid id) {
        return variables.ContainsKey(id);
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
