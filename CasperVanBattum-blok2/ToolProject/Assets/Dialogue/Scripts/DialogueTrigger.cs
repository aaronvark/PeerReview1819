using System;
using UnityEngine;

namespace Dialogue {
public class DialogueTrigger : MonoBehaviour {
    public const string DO_NOT_USE = "Do not use";
    
    [SerializeField] public string triggerButton;
    [SerializeField] private DialogueGraph dialogueGraph;
    
    private Conversation conversation;
    private bool conversationActive = false;

    private void Start() {
        conversation = new Conversation(dialogueGraph);
    }

    private void Update() {
        if (triggerButton != DO_NOT_USE && Input.GetButtonDown(triggerButton)) {
            Trigger();
        }
    }

    public void Trigger() {
        if (conversationActive) {
            conversation.MoveNext();
        }
        else {
            conversation.Start();
            conversationActive = true;
        }
    }
}
}
