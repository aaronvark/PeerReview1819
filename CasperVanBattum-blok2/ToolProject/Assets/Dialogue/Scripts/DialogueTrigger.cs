using UnityEngine;

namespace Dialogue {
public class DialogueTrigger : MonoBehaviour {
    [SerializeField] private DialogueGraph conversation;

    private bool triggerActive = false;
    
    private void Update() {
        if (Input.GetKeyDown("e")) {
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
}
