using Extensions;
using System;

namespace PublicDisplayer
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
