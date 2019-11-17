using System;
using UnityEngine;

namespace Dialogue {
public class DialogueTrigger : MonoBehaviour {
    public const string DO_NOT_USE = "Do not use";

    [SerializeField] public string triggerButton;
    [SerializeField] private DialogueGraph dialogueGraph;
    [SerializeField] private bool triggerActive = true;

    private Conversation conversation;

    private void Update() {
        if (!triggerActive) return;

        // TODO make better triggers
        if (triggerButton != DO_NOT_USE && Input.GetButtonDown(triggerButton)) {
            Trigger();
        }
    }

    public void Trigger() {
        if (conversation == null) {
            conversation = new Conversation(dialogueGraph);
            try {
                conversation.Start();
            }
            catch (InvalidOperationException) {
                // Start can throw an InvalidOperationException when the graph doesn't have an entry node. If this is
                // thrown, the trigger should be deactivated. TODO Avoid this using graph validation.
                triggerActive = false;
                throw;
            }
        }
        else if (conversation.Active) {
            conversation.NextNode();
        }

        // conversation.Active will be set to false during MoveNext() when the node after the current one is
        // nonexistent. In order to exit the conversation once the last node in the graph has been reached, it is
        // necessary to put an after-execution check on conversation.Active here.
        if (!conversation.Active) {
            // Reached end of dialogue graph
            Debug.Log("reached end of convo");
//            Destroy(this);
            triggerActive = false;
        }
    }
}
}
