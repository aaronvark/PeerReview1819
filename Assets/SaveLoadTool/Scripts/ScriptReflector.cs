using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

namespace Common.SaveLoadSystem
{
    public class ScriptReflector
    {
        public static List<string> GetVariableNames(Component component)
        {
            if (component.GetType() != typeof(SaveableIdentifier))
            {
                List<string> variableNames = new List<string>(); ;

                System.Type componentType = component.GetType();
                if (componentType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Length == 0 )
                {
                    PropertyInfo[] properties = componentType.GetProperties(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public);
                    for (int i = 0; i < properties.Length; i++)
                    {
                        variableNames.Add(properties[i].Name);
                    }
                } else
                {
                    FieldInfo[] fields = componentType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    for (int i = 0; i < fields.Length; i++)
                    {
                        variableNames.Add(fields[i].Name);
                    }
                }

                return variableNames;
            }
            return null;
        }
    }
}
