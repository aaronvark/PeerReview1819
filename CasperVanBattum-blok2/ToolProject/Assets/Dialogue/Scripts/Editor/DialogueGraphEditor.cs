using Dialogue;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace DialogueEditor {
[CustomNodeGraphEditor(typeof(DialogueGraph))]
public class DialogueGraphEditor : NodeGraphEditor {
    // Override method to check if a node of type EntryNode already exists in the graph, making the user unable to add
    // multiple entry nodes to the same graph via the node editor.
    // TODO change xNode to make this easier. Most of this method is copied right now while I only need to change the logic that adds the items.
    public override void AddContextMenuItems(GenericMenu menu) {
        var pos = NodeEditorWindow.current.WindowToGridPosition(Event.current.mousePosition);

        var graph = target as DialogueGraph;

        foreach (var type in NodeEditorReflection.nodeTypes) {
            //Get node context menu path
            var path = GetNodeMenuName(type);
            if (string.IsNullOrEmpty(path)) continue;

            // Allow only one node of type EntryNode per graph
            if (type == typeof(EntryNode) && graph.HasEntryNode()) {
                menu.AddDisabledItem(new GUIContent(path));
            }
            else {
                var type1 = type;
                menu.AddItem(new GUIContent(path), false, () => {
                    var node = CreateNode(type1, pos);
                    NodeEditorWindow.current.AutoConnect(node);
                });
            }
        }

        if (NodeEditorReflection.nodeTypes.Length > 0) menu.AddSeparator("");
        if (NodeEditorWindow.copyBuffer != null && NodeEditorWindow.copyBuffer.Length > 0)
            menu.AddItem(new GUIContent("Paste"), false, () => NodeEditorWindow.current.PasteNodes(pos));
        else
            menu.AddDisabledItem(new GUIContent("Paste"));
        menu.AddItem(new GUIContent("Preferences"), false, () => NodeEditorReflection.OpenPreferences());
        menu.AddCustomContextMenuItems(target);
    }
}
}
