using Extensions;
using System;

namespace NoStrings
{
    [Serializable]
    public class SubSettings
    {
        private string fileName;

        public string FileName
        {
            get => fileName;
            set => fileName = value.ToIdentifier();
        }

        public SubSettings(string fileName)
        {
            this.FileName = fileName;
        }
    }
}
