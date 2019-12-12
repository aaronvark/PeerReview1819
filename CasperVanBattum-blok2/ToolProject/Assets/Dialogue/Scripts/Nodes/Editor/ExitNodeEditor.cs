using UnityEngine;
using XNodeEditor;

namespace Dialogue.Editor {
[CustomNodeEditor(typeof(ExitNode))]
public class ExitNodeEditor : NodeEditor {
    public override void OnBodyGUI() {
        var node = (ExitNode) target;
        NodeEditorGUILayout.PortField(GUIContent.none, node.GetInputPort("inputNode"));
    }

    public override int GetWidth() {
        return 80;
    }

    public override Color GetTint() {
        return new Color32(246, 111, 128, 255);
    }
}
}
