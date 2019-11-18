using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.IMGUI.Controls;
using UnityEditor.ShortcutManagement;
using UnityEditorInternal;
using UnityEngine;

namespace Placer.PlacerTools
{
    [EditorTool("Duplicator", typeof(GameObject))]
    public class DuplicateTool : EditorTool
    {
        [SerializeField]
        private Texture2D toolIcon = null;

        public override GUIContent toolbarIcon => new GUIContent()
        {
            image = toolIcon,
            text = "Duplicator",
            tooltip =
                "Duplicate selection.\n" +
                "\n" +
                "- Hold Alt after clicking control handle to pin center in place.\n" +
                "- Hold Shift after clicking control handle to scale uniformly.",
        };

        public delegate void PerformedDuplicate();
        public PerformedDuplicate performedDuplicate = null; 

        private Bounds objectBounds, duplicationBounds = new Bounds();
        private BoxBoundsHandle boxBoundsHandle = new BoxBoundsHandle();

        [InitializeOnLoadMethod]
        [SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Called on initialization")]
        private static void RestorePreviousIfSelectedOnInitialize()
        {
            if(EditorTools.activeToolType == typeof(DuplicateTool))
                EditorTools.RestorePreviousPersistentTool();
        }

        [Shortcut("Tools/Duplicate", KeyCode.D)]
        [SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Called by the Shortcut Manager")]
        private static void SelectToolShortcut()
        {
            // Deselect the tool if it is selected, Select the tool if it can be selected.
            if(EditorTools.activeToolType == typeof(DuplicateTool))
                EditorTools.RestorePreviousPersistentTool();
            else if (Selection.gameObjects.Length > 0)
                EditorTools.SetActiveTool<DuplicateTool>();
        }

        private void OnEnable()
        {
            boxBoundsHandle.wireframeColor = Handles.zAxisColor;
            EditorTools.activeToolChanged += ResetBoundsIfSelected;
            EditorTools.activeToolChanging += DuplicateIfDeselected;
        }

        private void OnDisable()
        {
            EditorTools.activeToolChanged -= ResetBoundsIfSelected;
            EditorTools.activeToolChanging -= DuplicateIfDeselected;
        }

        private void ResetBoundsIfSelected()
        {
            if(EditorTools.IsActiveTool(this))
            {
                ResetBounds();
                Selection.selectionChanged += ResetBounds;
            }
        }

        private void DuplicateIfDeselected()
        {
            if(EditorTools.IsActiveTool(this))
            {
                Selection.selectionChanged -= ResetBounds;
                Duplicate();
            }
        }

        public void ResetBounds()
        {
            RecordObject("Bounds Reset");

            GameObject[] selectedGameObjects = Selection.gameObjects;
            if(selectedGameObjects.Length > 0)
            {
                objectBounds = InternalEditorUtility.CalculateSelectionBounds(true, false, false);

                foreach(var point in from GameObject gameObject in selectedGameObjects
                                     from Transform transform in gameObject.GetComponentsInChildren<Transform>()
                                     where transform.GetComponent<Renderer>() == null
                                     select transform.position)
                    objectBounds.Encapsulate(point);

                if(objectBounds.size == Vector3.zero)
                    objectBounds = new Bounds(selectedGameObjects[0].transform.position, Vector3.one);
            }
            else
            {
                objectBounds = default;
            }

            duplicationBounds.SetMinMax(objectBounds.center, objectBounds.center);
            boxBoundsHandle.center = objectBounds.center;
            boxBoundsHandle.size = objectBounds.size;
        }

        public void Duplicate()
        {
            if(Mathf.Approximately(objectBounds.size.x, 0)
                || Mathf.Approximately(objectBounds.size.y, 0)
                || Mathf.Approximately(objectBounds.size.z, 0))
                return;

            Object[] targetArray = targets.ToArray();
            Selection.objects = targetArray;

            Vector3 offset = new Vector3();
            for(offset.x = duplicationBounds.min.x; offset.x < duplicationBounds.max.x || Mathf.Approximately(offset.x, duplicationBounds.max.x); offset.x += objectBounds.size.x)
                for(offset.y = duplicationBounds.min.y; offset.y < duplicationBounds.max.y || Mathf.Approximately(offset.y, duplicationBounds.max.y); offset.y += objectBounds.size.y)
                    for(offset.z = duplicationBounds.min.z; offset.z < duplicationBounds.max.z || Mathf.Approximately(offset.z, duplicationBounds.max.z); offset.z += objectBounds.size.z )
                        if(offset != objectBounds.center)
                            InstantiateObjects(offset - objectBounds.center);

            Selection.objects = new Object[0];

            performedDuplicate?.Invoke();

            void InstantiateObjects(Vector3 translation)
            {
                SceneView.lastActiveSceneView.Focus();
                EditorWindow.focusedWindow.SendEvent(EditorGUIUtility.CommandEvent("Duplicate"));

                for(int i = 0; i < Selection.gameObjects.Length; ++i)
                {
                    GameObject go = Selection.gameObjects[i];
                    go.transform.Translate(translation, Space.World);
                }
            }
        }

        public override void OnToolGUI(EditorWindow window)
        {
            if(objectBounds.size == Vector3.zero)
                return;

            EditorGUI.BeginChangeCheck();

            boxBoundsHandle.DrawHandle();

            if(EditorGUI.EndChangeCheck())
            {
                Vector3 min = new Vector3();
                Vector3 max = new Vector3();

                for(int axis = 0; axis < 3; ++axis)
                {
                    float handleAxisMin = boxBoundsHandle.center[axis] - boxBoundsHandle.size[axis] / 2;
                    float handleAxisMax = handleAxisMin + boxBoundsHandle.size[axis];
                    float axisSize = objectBounds.size[axis];
                    float axisCenter = objectBounds.center[axis];

                    int behind = Mathf.Approximately(objectBounds.min[axis], handleAxisMin) ? 0 : Mathf.FloorToInt((objectBounds.min[axis] - handleAxisMin) / axisSize);
                    int ahead = Mathf.Approximately(handleAxisMax, objectBounds.max[axis]) ? 0 : Mathf.FloorToInt((handleAxisMax - objectBounds.max[axis]) / axisSize);

                    min[axis] = axisCenter - axisSize * behind;
                    max[axis] = axisCenter + axisSize * ahead;
                }

                duplicationBounds.SetMinMax(min, max);

                RecordObject("Bounds Changed");
            }

            Handles.color = Handles.xAxisColor;
            Handles.DrawWireCube(duplicationBounds.center, duplicationBounds.max - duplicationBounds.min + objectBounds.size);
        }

        private void RecordObject(string name)
        {
            Debug.Log(name);
            Undo.RegisterCompleteObjectUndo(this, name);
            Undo.FlushUndoRecordObjects();
            EditorUtility.SetDirty(this);
        }
    }
}
