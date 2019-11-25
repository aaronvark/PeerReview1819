using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using System.Xml;
using System.Linq;

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

        [RuntimeInitializeOnLoadMethod]
        public static void TestMethod()
        {
            LoadObject(4);
            //ConvertAllObjectsToXML();

        }

        public static GameObject LoadObject(int id)
        {
            SaveableIdentifier[] sceneObjects = Object.FindObjectsOfType<SaveableIdentifier>();
            GameObject obj = null;
            for (int i = 0; i < sceneObjects.Length; i++)
            {
                if (sceneObjects[i].id == id)
                {
                    obj = sceneObjects[i].gameObject;
                }
            }

            if (obj == null)
            {
                obj = new GameObject();
            }

            XmlNode idNode = GetIdNode(id);

            //Adds the component with correct values
            foreach (XmlNode cNode in idNode.ChildNodes)
            {
                string cName = cNode.Attributes[0].Value;
                System.Type cType = System.Type.GetType(cName);
                Component currentComponent = (obj.GetComponent(cType)) ? obj.GetComponent(cType) : obj.AddComponent(cType);

                foreach (XmlNode vNode in cNode.ChildNodes)
                {
                    ScriptReflector.SetComponentVarTo(
                        currentComponent,
                        vNode.Name,
                        ConvertString.ThisType(vNode.InnerText, System.Type.GetType(vNode.Attributes[0].Value)));
                }
            }
            return null;
        }


        #region Save functions

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

        private static void ConvertAllObjectsToXML()
        {
            SaveableIdentifier[] objects = Object.FindObjectsOfType<SaveableIdentifier>();

            for (int i = 0; i < objects.Length; i++)
            {
                AddToSave(objects[i].gameObject, objects[i].id);
            }

            xmlDocument.Save(saveFileName);
        }

        private static void AddToSave(GameObject obj, int id)
        {
            XmlNode idNode = GetCreateIdNode(id);
            List<ComponentSave> cSave = obj.GetComponent<Iidentifier>().componentSaves;

            foreach (Component component in obj.GetComponents<Component>())
            {
                ComponentSave componentSave = cSave.Find(x => x.componentTypeName == component.GetType().Name);

                if (
                    component.GetType() != typeof(SaveableIdentifier) &&
                    !DoesComponentNodeExist(idNode, component.GetType().AssemblyQualifiedName) &&
                    componentSave.boolItem == true
                    )
                {
                    MakeVariableNodes(component, MakeComponentNode(component, idNode), componentSave);
                }
            }
        }

        private static XmlNode MakeComponentNode(Component component, XmlNode parentNode)
        {
            XmlElement componentNode = xmlDocument.CreateElement("component");
            componentNode.SetAttribute("name", component.GetType().AssemblyQualifiedName);
            parentNode.AppendChild(componentNode);

            return componentNode;
        }

        private static void MakeVariableNodes(Component component, XmlNode componentNode, ComponentSave componentSave)
        {
            List<System.Tuple<string, string, string>> componentFields = ScriptReflector.GetVariableNames(component);

            for (int i = 0; i < componentFields.Count; i++)
            {
                if (componentSave.boolList[i])
                {
                    XmlElement fieldNode = xmlDocument.CreateElement(componentFields[i].Item1);
                    fieldNode.SetAttribute("type", componentFields[i].Item2);
                    fieldNode.InnerText = componentFields[i].Item3;
                    componentNode.AppendChild(fieldNode);
                }
            }
        }

        private static XmlNode GetCreateIdNode(int id)
        {
            if (xmlDocument.SelectSingleNode("saveables/" + idPrefix + id.ToString() + "[1]") == null)
            {
                XmlNode idNode = xmlDocument.CreateElement(idPrefix + id.ToString());
                rootNode.AppendChild(idNode);

                return idNode;
            }

            return xmlDocument.SelectSingleNode("saveables/" + idPrefix + id.ToString() + "[1]") ;
        }

        private static XmlNode GetIdNode(int id)
        {
            return xmlDocument.SelectSingleNode("saveables/" + idPrefix + id.ToString() + "[1]");
        }

        private static bool DoesComponentNodeExist(XmlNode parentNode, string nodeName)
        {
            foreach (XmlNode cNode in parentNode.ChildNodes)
            {
                if (cNode.Attributes[0].Value == nodeName)
                {
                    return true;
                }
            }

            return false;
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
        #endregion
    }
}
