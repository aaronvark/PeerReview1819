using System;
using UnityEngine;

namespace Dialogue {
public class ExitNode : DialogueBaseNode {
    [SerializeField, Input] private Empty inputNode;

    public override DialogueBaseNode GetNextNode() {
        throw new InvalidOperationException("GetNextNode() should not be called on an exit nodes");
    }
}
}
