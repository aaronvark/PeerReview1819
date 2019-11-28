using System;
using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ClassGenerator
{
    /// <summary>
    /// Generating the Script
    /// </summary>
    public void GenerateNode(UMLNode node)
    {
        Debug.Log(node.Functions.Count);
        string directoryPath = Environment.CurrentDirectory;

        using (StreamWriter sw = new StreamWriter(Path.Combine(directoryPath + "/Assets", node.ClassName + ".cs")))
        {
            //adding library's
            sw.WriteLine("using UnityEngine;");
            sw.WriteLine("using System.Collections;");
            sw.WriteLine("using System.Collections.Generic;");

            sw.WriteLine(""); //white space
            
            if (node.Parent != string.Empty)
                sw.WriteLine("public class " + node.ClassName + " : " + node.Parent);
            else
                sw.WriteLine("public class" + node.ClassName);

            sw.WriteLine("{");
            //adding variables
            foreach (string variable in node.Variables)
                sw.WriteLine(variable + ";");
            sw.WriteLine(""); //whitespace
            //adding functions
            foreach (string function in node.Functions)
            {
                if (function.EndsWith("()"))
                    sw.WriteLine(function + "{ }");
                else
                    sw.WriteLine(function + "() { }");
            }
            sw.WriteLine("}");
        }

        AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
    }

}
