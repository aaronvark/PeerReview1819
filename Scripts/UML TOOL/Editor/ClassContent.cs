namespace UnityEngine.Scripting.UML
{
    public class ClassContent
    {
        //You cant change struct variables in a list directly. you will get a "Cannot modify the return value"

        /// <summary>
        /// Access modifiers of the methodes/variable.
        /// </summary>
        public AccesModifiers AccesModifiers;

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
