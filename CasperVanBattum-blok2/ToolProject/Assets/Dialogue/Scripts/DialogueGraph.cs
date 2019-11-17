using System;
using UnityEngine;
using XNode;

namespace Dialogue {
[CreateAssetMenu]
public class DialogueGraph : NodeGraph {
    public EntryNode EntryNode => nodes.Find(node => node is EntryNode) as EntryNode;

    public bool HasEntryNode() {
        return EntryNode != null;
    }
}
}
