using System;
using UnityEngine;
using XNode;

namespace Dialogue {
[CreateAssetMenu]
public class DialogueGraph : NodeGraph {
    public EntryNode FindEntryNode() {
        var firstEntryNode = nodes.Find(node => node is EntryNode);
        return firstEntryNode as EntryNode;
    }

    public bool HasEntryNode() {
        return FindEntryNode() != null;
    }
}
}
