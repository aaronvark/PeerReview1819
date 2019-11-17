using System;
using UnityEngine;

namespace Dialogue {
/// <summary>
/// Conversation is a runtime version of a <see cref="DialogueGraph"/>. It contains all the runtime data that should not
/// be stored in the graph asset and it connects world state variables to the graph.
/// </summary>
public class Conversation {
    private readonly DialogueGraph dialogue;
    private DialogueBaseNode current;

    public bool Active { get; private set; }

    public Conversation(DialogueGraph dialogue) {
        this.dialogue = dialogue;
    }

    public void Start() {
        if (!current) {
            if (!(current = dialogue.FindEntryNode())) {
                throw new InvalidOperationException("No entry point in dialogue graph");
            }
        }

        MoveNext();
        Active = true;
    }

    public void MoveNext() {
        current = current.GetNextNode();

        DisplayNode(current);
        
        PeekNext();
    }

    private void PeekNext() {
        if (!current.GetNextNode()) {
            Active = false;
        }
    }

    private void DisplayNode(DialogueBaseNode node) {
        if (node is DialogueNode dialogueNode) {
            Debug.Log(dialogueNode.Text);
        }
    }
}
}
