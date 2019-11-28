namespace UnityEngine.Scripting.UML
{
    public class ClassContent
    {
        //<Attention>
        // You can't change struct variables in a list directly thats why I didn't use struct for this. 
        // You will get a "Cannot modify the return value".
        //</Attention>

        /// <summary>
        /// Access modifiers of the methodes/variable.
        /// </summary>
        public AccessModifiers AccessModifiers;

        /// <summary>
        /// Type of the variable.
        /// </summary>
        public string Type;

        /// <summary>
        /// Name of the methodes/variable.
        /// </summary>
        public string Name;
    }
}
