using System;

namespace WorldVariables {
[Serializable]
public class WorldVariable {
    public Guid Guid { get; }
    public string Name { get; set; }
    public VariableType Type { get; set; }
    public object Value { get; set; }

    public WorldVariable(string name, VariableType type, object value) {
        Guid = Guid.NewGuid();
        Name = name;
        Type = type;
        Value = value;
    }
}
}
