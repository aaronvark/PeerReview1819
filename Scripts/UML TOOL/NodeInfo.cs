﻿using System.Collections.Generic;
using UnityEditor;

namespace UnityEngine.Scripting.UML
{
    public class NodeInfo
    {
        public AccessModifiers ClassAccessModifiers;
        public string ClassName;
        public string Parent = string.Empty;

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

        /// <summary>
        /// Drawing the node
        /// </summary>
        public void Draw()
        {
            GUILayout.BeginVertical();
            GUILayout.Label("Class name");
            GUILayout.BeginHorizontal();
            ClassAccessModifiers = 
                (AccessModifiers)EditorGUILayout.EnumPopup((AccessModifiers)ClassAccessModifiers, GUILayout.MaxWidth(70));
            ClassName = GUILayout.TextField(ClassName);
            GUILayout.EndHorizontal();

            GUILayout.Label("Inheritance");
            Parent = GUILayout.TextField(Parent);
            
            GUILayout.Space(15);
            //variables
            GUILayout.Label("Variables \t Type \t Name");

            //Variable
            for (int i = 0; i < variableInfo.Count; i++)
            {
                ClassContent variable = VariableInfo[i];
                GUILayout.BeginHorizontal();
                //AccessModifier Type Variable 
                variable.AccessModifiers = (AccessModifiers)EditorGUILayout.EnumPopup((AccessModifiers)variable.AccessModifiers, GUILayout.MaxWidth(70) );
                variable.Type = GUILayout.TextField(variable.Type);
                variable.Name = GUILayout.TextField(variable.Name);
                
                if (GUILayout.Button("X", GUILayout.MaxWidth(20)))
                {
                    if (variableInfo.Count == 1)
                        VariableInfo = new List<ClassContent>();
                    VariableInfo.Remove(variableInfo[i]);
                }
                GUILayout.EndHorizontal();
            }

            //adding variable to the list.
            if (GUILayout.Button("Add variable", GUILayout.MaxWidth(100)))
                variableInfo.Add(new ClassContent());


            GUILayout.Space(15);

            //Methodes
            GUILayout.Label("Methods \t Type \t Name");
            for (int i = 0; i < methodeInfo.Count; i++)
            {
                ClassContent methode = methodeInfo[i];
                GUILayout.BeginHorizontal();
                //AccessModifier Type Methode 
                methode.AccessModifiers = (AccessModifiers)EditorGUILayout.EnumPopup((AccessModifiers)methode.AccessModifiers, GUILayout.MaxWidth(70));
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
            if (GUILayout.Button("Add methods", GUILayout.MaxWidth(100)))
                methodeInfo.Add(new ClassContent());

            GUILayout.Space(15);

            GUILayout.EndVertical();
        }
    }

    ///<summary>
    ///information of the classContent
    ///</summary>
    public enum AccessModifiers
    {
        Public,
        Private,
        Protected
    }
}