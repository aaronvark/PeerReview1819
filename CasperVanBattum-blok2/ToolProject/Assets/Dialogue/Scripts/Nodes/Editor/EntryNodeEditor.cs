using UnityEngine;
using XNodeEditor;

namespace Dialogue.Editor {
[CustomNodeEditor(typeof(EntryNode))]
public class EntryNodeEditor : NodeEditor {
    public override void OnBodyGUI() {
        var node = (EntryNode) target;
        NodeEditorGUILayout.PortField(GUIContent.none, node.GetOutputPort("outputNode"));
    }

    public override int GetWidth() {
        return 80;
    }

    public override Color GetTint() {
        return new Color32(111, 246,229, 255);
    }
}
}
