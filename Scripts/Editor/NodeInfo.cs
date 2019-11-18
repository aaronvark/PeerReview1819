using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace UnityEngine.Scripting.UML
{
    public class NodeInfo : MonoBehaviour
    {
        private Node node;
        public string ClassName;
        public string Parent;

        private List<ClassContent> methodeInfo = new List<ClassContent>();
        public List<ClassContent> MethodeInfo
        {
            get
            {
                return methodeInfo;
            }
            set
            {
                methodeInfo = value;
            }
        }

        private List<ClassContent> variableInfo = new List<ClassContent>();
        public List<ClassContent> VariableInfo
        {
            get
            {
                return variableInfo;
            }
            set
            {
                variableInfo = value;
            }
        }

        public NodeInfo(Node node)
        {
            this.node = node;
        }

        /// <summary>
        /// Updating the node info for inhieratance
        /// </summary>
        public void UpdateNode() { }

        /// <summary>
        /// Drawing the node
        /// </summary>
        public void Draw()
        {
            GUILayout.BeginVertical();
            GUILayout.Label("Class Name");
            ClassName = GUILayout.TextField(ClassName);
            GUILayout.Space(15);
            //variables
            GUILayout.Label("Variables");

            //Variable
            for (int i = 0; i < variableInfo.Count; i++)
            {
                ClassContent variable = VariableInfo[i];
                GUILayout.BeginHorizontal();
                //AccessModifier Type Variable 
                variable.AccesModifiers = (AccesModifiers)EditorGUILayout.EnumPopup((AccesModifiers)variable.AccesModifiers, GUILayout.MaxWidth(70));
                variable.Type = GUILayout.TextField(variable.Type);
                variable.Name = GUILayout.TextField(variable.Name);
                
                if (GUILayout.Button("X", GUILayout.MaxWidth(20)))
                {
                    if (variableInfo.Count == 1)
                        VariableInfo = new List<ClassContent>();
                    methodeInfo.Remove(variableInfo[i]);
                }
                GUILayout.EndHorizontal();
            }

            //adding variable to the list.
            if (GUILayout.Button("Add variable", GUILayout.MaxWidth(100)))
                variableInfo.Add(new ClassContent());


            GUILayout.Space(15);

            //Methodes
            GUILayout.Label("Methodes");
            for (int i = 0; i < methodeInfo.Count; i++)
            {
                ClassContent methode = methodeInfo[i];
                GUILayout.BeginHorizontal();
                //AccessModifier Type Methode 
                methode.AccesModifiers = (AccesModifiers)EditorGUILayout.EnumPopup((AccesModifiers)methode.AccesModifiers, GUILayout.MaxWidth(70));
                methode.Type = GUILayout.TextField(methode.Type);
                methode.Name = GUILayout.TextField(methode.Name);

                //methode.SetStructVariables(accesModifierTemp, typeTemp, nameTemp);

                if (GUILayout.Button("X",GUILayout.MaxWidth(20)))
                {
                    if (methodeInfo.Count == 1)
                        methodeInfo = new List<ClassContent>();
                    methodeInfo.RemoveAt(i);

                }
                GUILayout.EndHorizontal();
            }

            //adding methode to the list.
            if (GUILayout.Button("Add methode", GUILayout.MaxWidth(100)))
                methodeInfo.Add(new ClassContent());
            GUILayout.EndVertical();
        }
    }

    ///<summary>
    ///information of the classContent
    ///</summary>
    public struct ClassContent
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

    public enum AccesModifiers
    {
        PUBLIC,
        PRIVATE,
        PROTECTED
    }
}