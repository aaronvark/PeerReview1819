using System;
using System.Linq;
using Boo.Lang;
using UnityEditor;
using UnityEngine;
using WorldVariables;
using XNodeEditor;

namespace Dialogue.Editor {
[CustomNodeEditor(typeof(ConditionalTextNode))]
public class ConditionalTextEditor : TextNodeEditor {
    private int selectedIndex;

    protected override void DrawProperties(TextNode node) {
        base.DrawProperties(node);

//        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("condition"));
        var options = new List<string>();

        var worldVars = VariableCollection.Instance.OfType(VariableType.Bool);
        var worldVarsList = worldVars.ToList();
        foreach (var id in worldVarsList) {
            options.Add(VariableCollection.Instance.GetName(id));
        }

        var conditionalNode = (ConditionalTextNode) node;

        if (conditionalNode.VarId != Guid.Empty) {
            selectedIndex = worldVarsList.IndexOf(conditionalNode.VarId);
        }
        
        selectedIndex = EditorGUILayout.Popup("Condition", selectedIndex, options.ToArray());

        var value = (bool) VariableCollection.Instance.GetValue(worldVarsList[selectedIndex]);
        
        conditionalNode.VarId = worldVarsList[selectedIndex];
        
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.Toggle("Value", value);
        EditorGUI.EndDisabledGroup();

        serializedObject.ApplyModifiedProperties();
    }

}
}
