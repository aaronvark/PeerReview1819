using UnityEngine;

namespace Dialogue {
public class TextNode : DialogueBaseNode {
    [SerializeField, Input] private Empty inputNode;

    [SerializeField, TextArea] private string text;
    public string Text => text;
}
}
