#if UNITY_EDITOR
using UnityEditor;
using System.IO;
using System.Collections.Generic;

namespace EasyAI
{
    public enum AllEnums
    {

    }
    public class GenerateStruct : EditorWindow
    {
        private string structName;
        private List<AllEnums> allEnums;

        // Add menu named "NPCEditor" to the Window menu
        [MenuItem("Tools/GenerateStruct")]
        static void Init()
        {
            // Get existing open window or if none, make a new one:
            GenerateStruct window = (GenerateStruct)EditorWindow.GetWindow(typeof(GenerateStruct));
            window.Show();
            
        }
        public void Go()
        {
            
        }

    }
}
#endif

