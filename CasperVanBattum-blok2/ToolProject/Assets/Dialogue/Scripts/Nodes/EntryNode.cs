using UnityEngine;

namespace Dialogue {
public class EntryNode : DialogueBaseNode {
    [SerializeField, Output] private Empty outputNode;

    public override DialogueBaseNode GetNextNode() {
        var outPort = GetOutputPort("outputNode");
        if (outPort.Connection == null) {
            return null;
        }
        else {
            return outPort.Connection.node as DialogueBaseNode;
        }
    }
}
}
