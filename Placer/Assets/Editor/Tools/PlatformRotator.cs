using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;

namespace Placer.PlacerTools
{
    [EditorTool("Platform Rotator")]
    public class PlatformRotator : EditorTool
    {
        [SerializeField]
        private Texture2D toolIcon = null;

        public override GUIContent toolbarIcon => new GUIContent()
        {
            image = toolIcon,
            text = "Platform Rotator",
            tooltip = null,
        };

        public override void OnToolGUI(EditorWindow window)
        {
            EditorGUI.BeginChangeCheck();

            Vector3 position = Tools.handlePosition;
            Quaternion rotation = Tools.handleRotation;

            using(new Handles.DrawingScope(Color.green))
            {
                Handles.DrawWireDisc(position, Vector3.up, HandleUtility.GetHandleSize(position));

                //Handles.FreeRotateHandle(Quaternion.Euler(Vector3.up), position, HandleUtility.GetHandleSize(position));
                //Handles.RotationHandle(rotation, position);
                //Handles.RadiusHandle(rotation, Tools.handlePosition, 3f);
            }

            if(EditorGUI.EndChangeCheck())
            {

            }
        }
    }
}