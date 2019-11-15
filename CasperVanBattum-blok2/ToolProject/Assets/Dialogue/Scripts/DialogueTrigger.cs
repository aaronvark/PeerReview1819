using UnityEngine;

namespace Dialogue {
public class DialogueTrigger : MonoBehaviour {
    public const string DO_NOT_USE = "Do not use";
    
    [SerializeField] public string triggerButton;
    [SerializeField] private DialogueGraph conversation;

    private bool triggerActive = false;
    
    private void Update() {
        if (triggerButton == DO_NOT_USE && Input.GetButtonDown(triggerButton)) {
            Trigger();
        }
    }

    public void Trigger() {
        if (triggerActive) {
            conversation.MoveNext();
        }
        else {
            conversation.Start();
            triggerActive = true;
        }
    }
}
}
