using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;

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




