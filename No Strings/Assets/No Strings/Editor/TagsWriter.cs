using Extensions;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;

namespace NoStrings
{
    [InitializeOnLoad]
    public class TagsWriter : Writer
    {
        private const string fileName = "Tags";
        private static int tagsLength = 7;

        static TagsWriter()
        {
            EditorApplication.update += CheckTagsChange;
        }

        private static void CheckTagsChange()
        {
            string[] tags = InternalEditorUtility.tags;
            if(tagsLength != tags.Length)
                WriteTagsScript(tags);
        }

        public static void ChangeTags()
        {
            string[] tags = InternalEditorUtility.tags;
            WriteTagsScript(tags);
        }

        private static void WriteTagsScript(string[] tags)
        {
            tagsLength = tags.Length;

            string[] body = tags.ToIdentifiers(fileName).
                SelectMany((id, i) => ToVariable(id, tags[i])).
                Skip(1).ToArray();

            OverwriteOrAddFile(fileName, body, typeof(TagsWriter));
        }
    }
}
