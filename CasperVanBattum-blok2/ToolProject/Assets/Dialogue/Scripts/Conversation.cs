using System;
using UnityEngine;

namespace Dialogue {
/// <summary>
/// Conversation is a runtime version of a <see cref="DialogueGraph"/>. It contains all the runtime data that should not
/// be stored in the graph asset and it connects world state variables to the graph.
/// </summary>
public class Conversation {
    private readonly DialogueGraph dialogue;

    public Conversation(DialogueGraph dialogue) {
        this.dialogue = dialogue;
    }
    
    private DialogueBaseNode _current;

    private DialogueBaseNode Current {
        get {
            if (_current == null) {
                if ((_current = dialogue.FindEntryNode()) == null) {
                    throw new InvalidOperationException("No entry point in dialogue graph");
                }
            }

            return _current;
        }
        set => _current = value;
    }

    public void Start() {
        if (Current is EntryNode) MoveNext();
    }

    public void MoveNext() {
        Current = Current.GetNextNode();

        DisplayNode(Current);
    }
    
    private void DisplayNode(DialogueBaseNode node) {
        if (node is DialogueNode dialogueNode) {
            Debug.Log(dialogueNode.Text);
        }
    }
}
}
