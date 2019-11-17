using UnityEngine;
using XNode;

namespace Dialogue {
public abstract class DialogueBaseNode : Node {
    [SerializeField, Output] private Empty outputNode;

    public virtual DialogueBaseNode GetNextNode() {
        var outPort = GetOutputPort("outputNode");
        if (outPort.Connection == null) {
            return null;
        }
        else {
            return outPort.Connection.node as DialogueBaseNode;
        }
    }
}

[System.Serializable]
internal class Empty { }
}
