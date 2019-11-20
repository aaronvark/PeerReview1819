using XNodeEditor;

namespace Dialogue.Editor {
[CustomNodeEditor(typeof(ConditionalTextNode))]
public class ConditionalTextEditor : TextNodeEditor {
    protected override void DrawProperties(TextNode node) {
        base.DrawProperties(node);
        
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("condition"));
    }
}
}
