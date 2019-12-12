using System;
using System.Text.RegularExpressions;
using UnityEngine;
using WorldVariables;

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
            // FIXME shouldn't there be an error when the current is already set? Or just always reset the node to entry
            // If current is not set (which is highly likely when you're starting a conversation) try to assign the
            // entry node defined by the dialogue graph to the current node. If it is still null, scream.
            if (!(current = dialogue.EntryNode)) {
                throw new InvalidOperationException("No entry point in dialogue graph");
            }
        }

        NextNode();
        Active = true;
    }

    public void NextNode() {
        current = current.GetNextNode();

        // TODO replace this with an event that an interface can react to
        DisplayNode(current);

        PeekNext();
    }

    private void PeekNext() {
        // Deactivate the conversation once the graph has reached an exit node
        if (current.GetNextNode() is ExitNode) {
            Active = false;
        }
    }

    private void DisplayNode(DialogueBaseNode node) {
        if (node is TextNode) {
            if (node is IParsable parsableNode) {
                // TODO create an interface (or interface API) that will display the node instead of a debug statement
                Debug.Log(ParseText(parsableNode.Text));
            }
        }
    }

    private static string ParseText(string text) {
        var reg = new Regex("\\${[\\S\\s]+}");
        
        return reg.Replace(text, match => {
            var name = match.Value;
            // match is of form ${varname}, so remove the first two and the last character
            name = name.Substring(2, name.Length - 3);
            var worldVars = VariableCollection.Instance;
            return worldVars.GetValue(worldVars.GetGuidFromName(name)).ToString();
        });
    }
}
}
