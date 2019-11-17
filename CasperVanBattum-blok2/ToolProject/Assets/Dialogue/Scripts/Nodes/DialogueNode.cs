using UnityEngine;

namespace Dialogue {
public class DialogueNode : DialogueBaseNode {
    [SerializeField, Input] private Empty inputNode;
    [SerializeField, Output] private Empty outputNode;

    [SerializeField, TextArea] private string text;
    public string Text => text;
    
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
