using UnityEngine;

namespace Dialogue {
public class ConditionalDialogueNode : DialogueNode {
    [SerializeField] private bool condition;

    protected override DialogueBaseNode Get() {
        return condition ? this : GetNextNode();
    }
}
}
