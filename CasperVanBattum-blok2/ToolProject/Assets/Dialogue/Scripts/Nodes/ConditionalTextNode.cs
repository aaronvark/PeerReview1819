using System;
using UnityEngine;
using WorldVariables;

namespace Dialogue {
public class ConditionalTextNode : TextNode {
    [SerializeField] private string stringGuid;
    public Guid VarId {
        get => Guid.TryParse(stringGuid, out var res) ? res : Guid.Empty;
        set {
            stringGuid = value.ToString();
            Debug.Log($"Setting id to {value}");
        }
    }
    
    
    protected override DialogueBaseNode Get() {
        if (VariableCollection.Instance.VariableExists(VarId)) {
            var condition = (bool) VariableCollection.Instance.GetValue(VarId);

            return condition ? this : GetNextNode();
        }
        
        return GetNextNode();
    }
}
}
