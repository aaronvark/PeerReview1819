using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using System.Reflection;

namespace EasyAI
{
    /// <summary>
    /// static extensions for methods I often use
    /// </summary>
    public static class ExtensionMethods
    {
        public static RectTransform LerpRectTransform(this RectTransform rectTransform, MonoBehaviour owner, Vector3 targetPosition, float speed)
        {
            return ActivateCoroutineLerpRectTransformPositions(rectTransform, owner, targetPosition, speed);
        }

        public static Transform LerpTransform(this Transform currentTransform, MonoBehaviour owner, Vector3 targetPosition, float speed)
        {
            return ActivateCoroutineLerpTransformPositions(currentTransform, owner, targetPosition, speed);
        }

        public static List<T> ToList<T>(this T[] array) where T : class
        {
            List<T> output = new List<T>();
            output.AddRange(array);
            return output;
        }

        /// <summary>
        /// Perform a deep Copy of the object.
        /// </summary>
        /// <typeparam name="T">The type of object being copied.</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <returns>The copied object.</returns>
        public static T Clone<T>(this T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", nameof(source));
            }

            // Don't serialize a null object, simply return the default for that object
            if (System.Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        public static Vector3 ToZeroY(this Vector3 target)
        {
            return new Vector3(target.x, 0, target.z);
        }

        public static IEnumerable<SerializedProperty> GetChildren(this SerializedProperty property)
        {
            property = property.Copy();
            var nextElement = property.Copy();
            bool hasNextElement = nextElement.NextVisible(false);
            if (!hasNextElement)
            {
                nextElement = null;
            }

            property.NextVisible(true);
            while (true)
            {
                if ((SerializedProperty.EqualContents(property, nextElement)))
                {
                    yield break;
                }

                yield return property;

                bool hasNext = property.NextVisible(false);
                if (!hasNext)
                {
                    break;
                }
            }
        }

        public static Component AddComponentExt(this GameObject obj, string scriptName)
        {
            Component cmpnt = null;


            for (int i = 0; i < 10; i++)
            {
                //If call is null, make another call
                cmpnt = _AddComponentExt(obj, scriptName, i);

                //Exit if we are successful
                if (cmpnt != null)
                {
                    break;
                }
            }


            //If still null then let user know an exception
            if (cmpnt == null)
            {
                Debug.LogError("Failed to Add Component");
                return null;
            }
            return cmpnt;
        }

        private static Component _AddComponentExt(GameObject obj, string className, int trials)
        {
            //Any script created by user(you)
            const string userMadeScript = "Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null";
            //Any script/component that comes with Unity such as "Rigidbody"
            const string builtInScript = "UnityEngine, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null";

            //Any script/component that comes with Unity such as "Image"
            const string builtInScriptUI = "UnityEngine.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";

            //Any script/component that comes with Unity such as "Networking"
            const string builtInScriptNetwork = "UnityEngine.Networking, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";

            //Any script/component that comes with Unity such as "AnalyticsTracker"
            const string builtInScriptAnalytics = "UnityEngine.Analytics, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null";

            //Any script/component that comes with Unity such as "AnalyticsTracker"
            const string builtInScriptHoloLens = "UnityEngine.HoloLens, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null";

            Assembly asm = null;

            try
            {
                //Decide if to get user script or built-in component
                switch (trials)
                {
                    case 0:

                        asm = Assembly.Load(userMadeScript);
                        break;

                    case 1:
                        //Get UnityEngine.Component Typical component format
                        className = "UnityEngine." + className;
                        asm = Assembly.Load(builtInScript);
                        break;
                    case 2:
                        //Get UnityEngine.Component UI format
                        className = "UnityEngine.UI." + className;
                        asm = Assembly.Load(builtInScriptUI);
                        break;

                    case 3:
                        //Get UnityEngine.Component Video format
                        className = "UnityEngine.Video." + className;
                        asm = Assembly.Load(builtInScript);
                        break;

                    case 4:
                        //Get UnityEngine.Component Networking format
                        className = "UnityEngine.Networking." + className;
                        asm = Assembly.Load(builtInScriptNetwork);
                        break;
                    case 5:
                        //Get UnityEngine.Component Analytics format
                        className = "UnityEngine.Analytics." + className;
                        asm = Assembly.Load(builtInScriptAnalytics);
                        break;

                    case 6:
                        //Get UnityEngine.Component EventSystems format
                        className = "UnityEngine.EventSystems." + className;
                        asm = Assembly.Load(builtInScriptUI);
                        break;

                    case 7:
                        //Get UnityEngine.Component Audio format
                        className = "UnityEngine.Audio." + className;
                        asm = Assembly.Load(builtInScriptHoloLens);
                        break;

                    case 8:
                        //Get UnityEngine.Component SpatialMapping format
                        className = "UnityEngine.VR.WSA." + className;
                        asm = Assembly.Load(builtInScriptHoloLens);
                        break;

                    case 9:
                        //Get UnityEngine.Component AI format
                        className = "UnityEngine.AI." + className;
                        asm = Assembly.Load(builtInScript);
                        break;
                }
            }
            catch (Exception e)
            {
                //Debug.Log("Failed to Load Assembly" + e.Message);
            }

            //Return if Assembly is null
            if (asm == null)
            {
                return null;
            }

            //Get type then return if it is null
            Type type = asm.GetType(className);
            if (type == null)
                return null;

            //Finally Add component since nothing is null
            Component cmpnt = obj.AddComponent(type);
            return cmpnt;
        }

    public static System.Type ObjectToClassType(this UnityEngine.Object myObject)
        {
            MonoScript tmpMonoHolder = myObject as MonoScript;
            return tmpMonoHolder.GetClass();
        }
    

    public static RectTransform ActivateCoroutineLerpRectTransformPositions(RectTransform rectTransform, MonoBehaviour owner, Vector3 targetPosition, float speed)
        {
            if (rectTransform == null) return null;

            if (owner != null)
            {
                owner.StartCoroutine(ExtensionHelpers.LerpRectTransformPositions(rectTransform, targetPosition, speed));
                return rectTransform;
            }
            else
            {
                Debug.Log("Our Owner is null");
                return null;
            }
        }

        public static Transform ActivateCoroutineLerpTransformPositions(Transform currentTransform, MonoBehaviour owner, Vector3 targetPosition, float speed)
        {
            if (currentTransform == null) return null;

            if (owner != null)
            {
                owner.StartCoroutine(ExtensionHelpers.LerpTransformPositions(currentTransform, targetPosition, speed));
                return currentTransform;
            }
            else
            {
                Debug.Log("Our Owner is null");
                return null;
            }
        }
    }
}




