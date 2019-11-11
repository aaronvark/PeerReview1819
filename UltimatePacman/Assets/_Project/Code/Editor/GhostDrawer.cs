using UnityEditor;
using UnityEditor.AnimatedValues;

[CustomEditor(typeof(Ghost))]
[CanEditMultipleObjects]
public class GhostDrawer : Editor
{
    SerializedProperty behaviour;
    AnimBool showFlankBase;
    SerializedProperty flankBase;
    SerializedProperty consumedClip;
    SerializedProperty scoreValue;

    private bool BehaviourIsFlank => (behaviour.enumValueIndex == (int)GhostBehaviour.Flank);

    private void OnEnable()
    {
        behaviour = serializedObject.FindProperty("behaviour");
        showFlankBase = new AnimBool(BehaviourIsFlank);
        flankBase = serializedObject.FindProperty("flankBase");
        consumedClip = serializedObject.FindProperty("consumedClip");
        scoreValue = serializedObject.FindProperty("scoreValue");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(behaviour, true);
        showFlankBase.target = BehaviourIsFlank;

        if(EditorGUILayout.BeginFadeGroup(showFlankBase.faded))
            EditorGUILayout.PropertyField(flankBase, true);
        EditorGUILayout.EndFadeGroup();
        EditorGUILayout.PropertyField(consumedClip, true);
        EditorGUILayout.PropertyField(scoreValue, true);

        serializedObject.ApplyModifiedProperties();
    }
}
