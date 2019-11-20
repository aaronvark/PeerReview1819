using UnityEngine;
using XNodeEditor;

namespace Dialogue.Editor {
[CustomNodeEditor(typeof(TextNode))]
public class TextNodeEditor : NodeEditor {
    public override void OnBodyGUI() {
        serializedObject.Update();

        var node = (TextNode) target;
        
        GUILayout.BeginHorizontal();
        {
            NodeEditorGUILayout.PortField(GUIContent.none, target.GetInputPort("inputNode"),GUILayout.MinWidth(0));
            GUILayout.Label("Enter text");
            NodeEditorGUILayout.PortField(GUIContent.none, target.GetOutputPort("outputNode"),GUILayout.MinWidth(0));
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(-10);
        
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("text"), GUIContent.none);
        
        DrawProperties(node);

        serializedObject.ApplyModifiedProperties();
    }

    protected virtual void DrawProperties(TextNode node) { }
}
}
