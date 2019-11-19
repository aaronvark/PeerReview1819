using UnityEngine;
using XNode;

namespace Dialogue {
public abstract class DialogueBaseNode : Node {
    [SerializeField, Output] private Empty outputNode;

    public virtual DialogueBaseNode GetNextNode() {
        var outPort = GetOutputPort("outputNode");

        return ((DialogueBaseNode) outPort.Connection?.node)?.Get();
    }

    public override object GetValue(NodePort port) {
        return null;
    }
    
    protected virtual DialogueBaseNode Get() {
        return this;
    }
}

[System.Serializable]
internal class Empty { }
}
