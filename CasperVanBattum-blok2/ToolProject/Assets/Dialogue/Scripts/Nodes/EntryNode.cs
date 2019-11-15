using UnityEngine;

namespace Dialogue {
public class EntryNode : DialogueBaseNode {
    [SerializeField, Output] private Empty output;

    public override DialogueBaseNode GetNextNode() {
        var outPort = GetOutputPort("output");
        return (DialogueBaseNode) outPort?.Connection.node;
    }
}
}
