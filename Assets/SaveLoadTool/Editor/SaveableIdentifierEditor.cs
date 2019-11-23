using UnityEngine;
using UnityEditor;
using Common.SaveLoadSystem;

[CustomEditor(typeof(SaveableIdentifier))]
public class SaveableIdentifierEditor : Editor
{
    private void OnEnable()
    {
        SaveableIdentifier id = target as SaveableIdentifier;
        //target.hideFlags = HideFlags.HideInInspector;         //TODO: uncomment me when done
        target.hideFlags = HideFlags.NotEditable;
    }
}
