using System;

namespace WorldVariables {
[Serializable]
public class WorldVariable {
    private string name;
    private VariableType type;
    private object value;
    
    public Guid Guid { get; }

    public string Name {
        get => name;
        set {
            name = value;
            NameChanged?.Invoke(value);
        }
    }

    public VariableType Type {
        get => type;
        set {
            // Invoke type change with old type and new type
            var oldType = type;
            // Direct value change bypasses event that is fired from external value changes through property 
            this.value = ConvertValue(this.value, oldType, value);
            
            type = value;
            TypeChanged?.Invoke(oldType, value);
        }
    }

    public object Value {
        get => value;
        set {
            this.value = value;
            ValueChanged?.Invoke(value);
        }
    }

    [field:NonSerialized] public event Action<string> NameChanged;
    [field:NonSerialized] public event Action<VariableType, VariableType> TypeChanged;
    [field:NonSerialized] public event Action<object> ValueChanged;

    public WorldVariable(string name, VariableType type, object value) {
        Guid = Guid.NewGuid();
        this.name = name;
        this.type = type;
        this.value = value;
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
