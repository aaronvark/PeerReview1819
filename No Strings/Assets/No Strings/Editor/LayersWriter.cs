using Extensions;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace NoStrings
{
    [InitializeOnLoad]
    public class LayersWriter : Writer
    {
        private const string fileName = "Layers";

        private static string[] lastLayers = 
        {
            "Default",
            "TransparentFX",
            "Ignore Raycast",
            "Water",
            "UI"
        };

        static LayersWriter()
        {
            EditorApplication.update += CheckLayersChange;
        }

        // If the layers have changed, rewrite the Layers.cs file
        private static void CheckLayersChange()
        {
            string[] currentLayers = InternalEditorUtility.layers;
            if(!lastLayers.SequenceEqual(currentLayers))
                WriteLayersScript(currentLayers);
        }

        public static void ChangeLayers()
        {
            string[] currentLayers = InternalEditorUtility.layers;
            WriteLayersScript(currentLayers);
        }

        private static void WriteLayersScript(string[] layers)
        {
            lastLayers = layers.ToArray();

            // Remove unusable layers
            IEnumerable<string> fixedLayers = layers.Distinct();
            if(LayerMask.NameToLayer(layers.Last()) == 31)
            {
                Debug.LogWarning("Layer 31 is used internally by the Editor’s Preview window mechanics." +
                " To prevent clashes, do not use this layer.");
                fixedLayers = fixedLayers.Take(fixedLayers.Count() - 1);
            }
            layers = fixedLayers.ToArray();

            List<string> body = layers.ToIdentifiers(fileName).
                SelectMany((id, i) => ToVariable(id, layers[i])).
                Skip(1).ToList();

            // Write Bitwise Operator Methods with Summary's
            body.AddRange(
                Method(
                    "Returns the shared layers.",
                    "Intersect",
                    "(int layer1, int layer2, params int[] otherLayers)",
                    "otherLayers.Concat(new []{layer1, layer2}).Aggregate((current, next) => current & next)"
                )
            );

            body.AddRange(
                Method(
                    "Returns all layers as one.",
                    "Combine",
                    "(int layer1, int layer2, params int[] otherLayers)",
                    "otherLayers.Concat(new []{layer1, layer2}).Aggregate((current, next) => current | next)"
                )
            );

            body.AddRange(
                Method(
                    "Returns the exclusive layers.",
                    "Difference",
                    "(int layer1, int layer2)",
                    "layer1 ^ layer2"
                )
            );

            body.AddRange(
                Method(
                    "Returns all other layers.",
                    "Inverse",
                    "(int layer)",
                    "~layer"
                )
            );

            body.AddRange(
                Method(
                    "Returns the layers shift steps after this layer.",
                    "Next",
                    "(int layer, int shift)",
                    "layer << shift"
                )
            );

            body.AddRange(
                Method(
                    "Returns the layers shift steps before this layer.",
                    "Previous",
                    "(int layer, int shift)",
                    "layer >> shift"
                )
            );

            // Write Layers
            OverwriteOrAddFile(fileName, body, typeof(LayersWriter), "System.Linq");

            // Write Method Simplifier
            string[] Method(string summary, string methodName, string parameters, string method)
            {
                string methodIdentifier = $"public static int {methodName}";
                return new[]{
                    "",
                    "/// <summary>",
                    $"/// {summary}",
                    "/// </summary>",
                    $"{methodIdentifier}{parameters} => {method};",
                };
            }
        }
    }
}
