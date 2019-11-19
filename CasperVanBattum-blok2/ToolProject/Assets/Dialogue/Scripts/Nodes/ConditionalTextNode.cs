using UnityEngine;

namespace Dialogue {
public class ConditionalTextNode : TextNode {
    [SerializeField] private bool condition;

    protected override DialogueBaseNode Get() {
        return condition ? this : GetNextNode();
    }
}
}
