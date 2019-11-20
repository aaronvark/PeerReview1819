using UnityEditor;
using UnityEngine;

namespace EasyAI
{
    [System.Serializable]
    public class AnimationData : MonoBehaviour, ISetting
    {
        [Header("MovementAnimations:")]
        [SerializeField]
        public AnimationClip IdleAnimation;
        [SerializeField]
        public AnimationClip WalkAnimation;
        [SerializeField]
        public AnimationClip RunAnimation;

        [Header("CombatAnimations:")]
        [SerializeField]
        public AnimationClip AttackAnimation;
        [SerializeField]
        public AnimationClip DeathAnimation;

        public void RenderUI(SerializedProperty property)
        {
            var idleAnimation = property.FindPropertyRelative("IdleAnimation");
            var walkAnimation = property.FindPropertyRelative("WalkAnimation");
            var runAnimation = property.FindPropertyRelative("RunAnimation");
            var attackAnimation = property.FindPropertyRelative("AttackAnimation");
            var deathAnimation = property.FindPropertyRelative("DeathAnimation");
            EditorGUILayout.LabelField("MovementAnimations:");
            EditorGUILayout.PropertyField(idleAnimation);
            EditorGUILayout.PropertyField(walkAnimation);
            EditorGUILayout.PropertyField(runAnimation);
            EditorGUILayout.LabelField("CombatAnimations:");
            EditorGUILayout.PropertyField(attackAnimation);
            EditorGUILayout.PropertyField(deathAnimation);
        }

        private Animator animator;
        private AnimatorControllerParameter animatorController;
        private void Start()
        {
            animator = GetComponent<Animator>();
            animatorController = new AnimatorControllerParameter();
        }
        public System.Type GetChildType()
        {
            return this.GetType();
        }
    }
}
