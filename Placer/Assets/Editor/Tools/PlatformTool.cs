using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;

namespace Placer.PlacerTools
{
    // Tagging a class with the EditorTool attribute and no target type registers a global tool. Global tools are valid for any selection, and are accessible through the top left toolbar in the editor.
    [EditorTool("Platform Tool")]
    class PlatformTool : EditorTool
    {
        private const float slide2DSize = 0.125f;

        [SerializeField]
        private Texture2D toolIcon = null;

        public override GUIContent toolbarIcon => new GUIContent()
        {
            image = toolIcon,
            text = "Platform Placer",
            tooltip = null,
        };

        // This is called for each window that your tool is active in. Put the functionality of your tool here.
        public override void OnToolGUI(EditorWindow window)
        {
            EditorGUI.BeginChangeCheck();

            Vector3 position = Tools.handlePosition;

            using(new Handles.DrawingScope())
            {
                float handleSize = HandleUtility.GetHandleSize(position);

                Handles.color = Handles.xAxisColor;
                position = Handles.Slider(position, Vector3.right);

                Handles.color = Handles.zAxisColor;
                position = Handles.Slider(position, Vector3.forward);

                Handles.color = Handles.yAxisColor;
                position = Handles.Slider2D(position + new Vector3(1, 0, 1) * handleSize * slide2DSize, Vector3.up, Vector3.right, Vector3.forward, handleSize * slide2DSize, Handles.RectangleHandleCap, 0);
            }

            if(EditorGUI.EndChangeCheck())
            {
                Vector3 delta = Snapping.Snap(position - Tools.handlePosition, EditorSnapSettings.move);

                Undo.RecordObjects(Selection.transforms, "Move Platform");

                foreach(var transform in Selection.transforms)
                    transform.position += delta;
            }
        }
    }
}