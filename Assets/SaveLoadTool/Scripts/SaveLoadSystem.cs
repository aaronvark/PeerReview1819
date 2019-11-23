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
        private static XmlNode rootNode;
        private static string saveFileName = "Save.xml";
        private static string rootNodeName = "saveables";
        private static string idPrefix = "id";


        static SaveLoadSystem()
        {
            try
            {
                xmlDocument.Load(saveFileName);
                rootNode = xmlDocument.FirstChild;
            } catch (FileNotFoundException)
            {
                CreateNewFile();
            }
        }

        public static int GetId()
        {
            string id = rootNode.Attributes["id"].Value;
            int newValue = int.Parse(id) + 1;
            rootNode.Attributes["id"].Value = newValue.ToString();
            xmlDocument.Save(saveFileName);
            return newValue;
        }

        [RuntimeInitializeOnLoadMethod]
        private static void ConvertAllObjectsToXML()
        {
            SaveableIdentifier[] objects = Object.FindObjectsOfType<SaveableIdentifier>();

            for (int i = 0; i < objects.Length; i++)
            {
                AddToSave(objects[i].gameObject, objects[i].GetId());
            }

            xmlDocument.Save(saveFileName);
        }

        private static void AddToSave(GameObject obj, int id)
        {
            XmlNode idNode = GetNode(id);

            foreach (Component component in obj.GetComponents<Component>())
            {
                MakeComponentNode(component, idNode);
            }
        }

        private static void MakeComponentNode(Component component, XmlNode parentNode)
        {
            XmlElement componentNode = xmlDocument.CreateElement("component");
            componentNode.SetAttribute("name", component.GetType().Name);
            parentNode.AppendChild(componentNode);

            List<string> componentFields = ScriptReflector.GetVariableNames(component);
            for (int i = 0; i < componentFields.Count; i++)
            {
                XmlElement fieldNode = xmlDocument.CreateElement(componentFields[i]);
                componentNode.AppendChild(fieldNode);
            }
        }

        private static XmlNode GetComponentNodeFrom(XmlNode xmlNode, string name)
        {
            //if(xmlNode.SelectSingleNode(NameToXMLComponent(name)) == null)
            //{
            //    xmlNode.AppendChild(make);
            //    return newNode;
            //}

            return xmlNode.SelectSingleNode(NameToXMLComponent(name));
        }

        private static XmlNode GetNode(int id)
        {
            if (xmlDocument.SelectSingleNode("saveables/" + idPrefix + id.ToString() + "[1]") == null)
            {
                XmlNode idNode = xmlDocument.CreateElement(idPrefix + id.ToString());
                rootNode.AppendChild(idNode);

                return idNode;
            }

            return xmlDocument.SelectSingleNode("saveables/" + idPrefix + id.ToString() + "[1]") ;
        }

        private static string NameToXMLComponent(string componentName)
        {
            return "component[@name= " + "'" + componentName + "'" + "][1]";
        }

        private static void CreateNewFile()
        {
            XmlElement rootElement = xmlDocument.CreateElement(rootNodeName);
            rootElement.SetAttribute("id", Object.FindObjectsOfType<SaveableIdentifier>().Length.ToString());
            xmlDocument.AppendChild(rootElement);

            xmlDocument.Save(saveFileName);
            xmlDocument.Load(saveFileName);
            rootNode = xmlDocument.FirstChild;
        }
    }
}
