using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using System.Xml;

namespace Common.SaveLoadSystem
{
    [InitializeOnLoad]
    public class SaveLoadSystem
    {
        private static XmlDocument xmlDocument = new XmlDocument();
        
        static SaveLoadSystem()
        {
            try
            {
                xmlDocument.Load("Save.xml");
            } catch (FileNotFoundException)
            {
                CreateNewFile();
            }

            string[] yeet = Directory.GetFiles(Application.dataPath, "SaveLoadSystem.cs", SearchOption.AllDirectories);
            yeet[0] = yeet[0].Replace(Application.dataPath.Replace('\\', '/'), "");
        }

        public static bool IsNodeExisting(string objectName)
        {
            foreach (XmlNode node in xmlDocument.DocumentElement.ChildNodes)
            {
                if (node.Name == ConvertStringTo.XMLFormat(objectName))
                {
                    return true;
                }
            }
            return false;
        }

        public static XmlNode GetXMLNode(string name)
        {
            foreach (XmlNode node in xmlDocument.DocumentElement.ChildNodes)
            {
                if (node.Name == ConvertStringTo.XMLFormat(name))
                {
                    return node;
                }
            }
            return null;
        }

        public static void AddToSave(Transform obj)
        {
            XmlNode xmlNode = null;
            if (IsNodeExisting(obj.name))
            {
                xmlNode = xmlDocument.CreateElement(ConvertStringTo.XMLFormat(obj.name));
                xmlDocument.DocumentElement.AppendChild(xmlNode);
            } else
            {
                GetXMLNode(ConvertStringTo.XMLFormat(obj.name));
            }

            XmlNode positionNode = xmlDocument.CreateElement("position");
            positionNode.InnerText = obj.position.ToString();
            xmlNode.AppendChild(positionNode);

            xmlDocument.Save("Save.xml");
        }

        private static void CreateNewFile()
        {
            xmlDocument.AppendChild(xmlDocument.CreateElement("Transforms"));
            xmlDocument.Save("Save.xml");
        }

        [RuntimeInitializeOnLoadMethod]
        public static void LoadList()
        {
            foreach (XmlNode node in xmlDocument.DocumentElement.ChildNodes)
            {
                GameObject newObj = new GameObject(node.Name.Replace('_', ' '));
                newObj.transform.position = ConvertStringTo.Vector3(node.SelectSingleNode("position").InnerText);
            }
        }
    }
}
