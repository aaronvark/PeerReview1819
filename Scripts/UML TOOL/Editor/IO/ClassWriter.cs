using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace UnityEngine.Scripting.UML
{
    public class ClassWriter : ICodeGenerator
    {
        public void GenerateClass(NodeInfo nodeInfo)
        {
            string directoryPath = Environment.CurrentDirectory + "/Assets";

            using (StreamWriter sw = new StreamWriter(Path.Combine(directoryPath, nodeInfo.ClassName + ".cs")))
            {

                //adding library's
                sw.WriteLine("using UnityEngine;");
                sw.WriteLine("using System.Collections;");
                sw.WriteLine("using System.Collections.Generic;");

                sw.WriteLine(""); //white space

                sw.WriteLine("/// <Information>");
                sw.WriteLine("/// This class is generated with the UML tool from Geoffrey Hendrikx.\n");
                sw.WriteLine("/// </Information>");

                if (nodeInfo.Parent != string.Empty)
                    sw.WriteLine(nodeInfo.classAccesModifiers.ToString().ToLower() + " class " + nodeInfo.ClassName + " : " + nodeInfo.Parent);
                else
                    sw.WriteLine(nodeInfo.classAccesModifiers.ToString().ToLower() + " class " + nodeInfo.ClassName);

                sw.WriteLine("{");
                //adding variables
                for (int i = 0; i < nodeInfo.VariableInfo.Count; i++)
                {
                    ClassContent variable = nodeInfo.VariableInfo[i];
                    sw.WriteLine("\t" + variable.AccesModifiers.ToString().ToLower() + " " + variable.Type + " " + variable.Name + ";");
                }

                sw.WriteLine(" "); // whitespace

                for (int i = 0; i < nodeInfo.MethodeInfo.Count; i++)
                {
                    ClassContent methode = nodeInfo.MethodeInfo[i];
                    if (methode.Name.EndsWith(")"))
                        sw.WriteLine("\t" + methode.AccesModifiers.ToString().ToLower() + " " + methode.Type + " " + methode.Name + "{ }");
                    else
                        sw.WriteLine("\t" + methode.AccesModifiers.ToString().ToLower() + " " + methode.Type + " " + methode.Name + "() { }");
                }

                sw.WriteLine("}");

                sw.Flush();

                sw.Close();
            }
            AssetDatabase.Refresh();
        }
    }

}