using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace PublicDisplayer
{
    [CustomEditor(typeof(PublicDisplayerSettings))]
    [CanEditMultipleObjects]
    public class PublicDisplayerSettingEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginDisabledGroup(true);
            base.OnInspectorGUI();
            EditorGUI.EndDisabledGroup();
        }
    }
}
