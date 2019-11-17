using UnityEngine;

namespace Dialogue {
public class ConditionalDialogueNode : DialogueNode {
    [SerializeField] private bool condition;

    public override DialogueBaseNode Get() {
        return condition ? this : GetNextNode();
    }
}
}
