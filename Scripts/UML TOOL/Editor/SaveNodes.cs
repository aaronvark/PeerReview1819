using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.Scripting.UML
{
    public class SaveNodes : ScriptableObject
    {
        public List<Node> Nodes;
        public Inheritance Inheritance;
        [HideInInspector]
        public bool SetInheritance;
    }
}
