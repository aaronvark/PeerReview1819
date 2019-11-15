using System;
using UnityEngine;
using XNode;

namespace Dialogue {
[CreateAssetMenu]
public class DialogueGraph : NodeGraph {
    [NonSerialized] private DialogueBaseNode _current;

    private DialogueBaseNode Current {
        get {
            if (_current == null) {
                if ((_current = FindEntryNode()) == null) {
                    throw new InvalidOperationException("No entry point in dialogue graph");
                }
            }

            return _current;
        }
        set => _current = value;
    }

    public void Start() {
//        Current = FindEntryNode();
        if (Current is EntryNode) MoveNext();
    }

    public void MoveNext() {
        Current = Current.GetNextNode();

        DisplayNode(Current);
    }

    private void DisplayNode(Node node) {
        if (node is DialogueNode dialogueNode) {
            Debug.Log(dialogueNode.Text);
        }
    }

    private EntryNode FindEntryNode() {
        var firstEntryNode = nodes.Find((node) => node is EntryNode);
        return (EntryNode) firstEntryNode;
    }
}
}
