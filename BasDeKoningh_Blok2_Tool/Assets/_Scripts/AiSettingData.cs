using UnityEditor;
using UnityEngine;

namespace EasyAI
{
    [System.Serializable]
    public class AiSettingData : MonoBehaviour, ISetting
    {
        [SerializeField]
        public float Damage;
        [SerializeField]
        public float Health;
        [SerializeField]
        public float Range;
        [SerializeField]
        public float Speed;
        [SerializeField]
        public AiType AiType;

        public void RenderUI(SerializedProperty property)
        {
            Debug.Log(this.GetType().Assembly);

            var damage = property.FindPropertyRelative("Damage");
            var health = property.FindPropertyRelative("Health");
            var range = property.FindPropertyRelative("Range");
            var aiType = property.FindPropertyRelative("AiType");
            EditorGUILayout.PropertyField(damage);
            EditorGUILayout.PropertyField(health);
            EditorGUILayout.PropertyField(range);
            EditorGUILayout.PropertyField(aiType);
        }


        public System.Type GetChildType()
        {
            return this.GetType();
        }
    }
}
