using System;
using UnityEngine;
using WorldVariables;

namespace Dialogue {
public class ConditionalTextNode : TextNode {
    public Guid VarId { get; set; }    

    protected override DialogueBaseNode Get() {
        if (VariableCollection.Instance.VariableExists(VarId)) {
            var condition = (bool) VariableCollection.Instance.GetValue(VarId);

            return condition ? this : GetNextNode();
        }
        
        return GetNextNode();
    }
}
}
