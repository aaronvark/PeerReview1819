using UnityEngine;
using XNode;

namespace Dialogue {
public class DialogueNode : DialogueBaseNode {
    [SerializeField, Input] private Empty inputNode;
    [SerializeField, Output] private Empty outputNode;

    [SerializeField, TextArea] private string text;
    public string Text => text;
    
    public override DialogueBaseNode GetNextNode() {
        var outPort = GetOutputPort("outputNode");
        return (DialogueBaseNode) outPort?.Connection.node;
    }
}

[System.Serializable]
internal class Empty { }
}
