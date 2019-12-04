using Extensions;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;

namespace NoStrings
{
    [InitializeOnLoad]
    public class ControllersWriter : Writer
    {
        private const string fileName = "Controllers";
        private static string[] lastGuids = { };

        static ControllersWriter()
        {
            EditorApplication.update += CheckControllersChange;
        }

        private static void CheckControllersChange()
        {
            string[] currentGuids = AssetDatabase.FindAssets("t:animatorController");
            if(!lastGuids.SequenceEqual(currentGuids))
                WriteControllersScripts(currentGuids);
        }

        public static void ChangeControllers()
        {
            string[] currentGuids = AssetDatabase.FindAssets("t:animatorController");
            WriteControllersScripts(currentGuids);
        }

        private static void WriteControllersScripts(string[] guids)
        {
            lastGuids = guids;

            // Retrieve all AnimatorControllers
            AnimatorController[] controllers = guids.Select(guid =>
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                return AssetDatabase.LoadAssetAtPath<AnimatorController>(assetPath);
            }).ToArray();

            LinkedList<string> body = new LinkedList<string>(
                controllers.Select(c => c.name).
                ToIdentifiers(fileName)
            );

            // Loop through all animator controllers in reverse <= technique used a lot to make filling the list easier.
            for(int c = body.Count - 1; c >= 0; c--)
            {
                // Get instances of important variables
                LinkedListNode<string> controllerNode = body.First;
                for(int n = 0; n < c; n++)
                    controllerNode = controllerNode.Next;
                var controller = controllers[c];

                LinkedList<string> values = new LinkedList<string>
                (
                    (new[] { "Parameters", "Tags" }.
                    Concat(controller.layers.Select(l => l.name))).
                    ToIdentifiers(controllerNode.Value)
                );

                // Fill out the values for the controller
                IndentLevel++;
                {
                    HashSet<string> tags = new HashSet<string>();

                    // Fill out the layers for the values
                    for(int l = controller.layers.Length - 1; l >= 0; l--)
                    {
                        LinkedListNode<string> layerNode = values.First;
                        for(int n = 0; n < l + 2; n++)
                            layerNode = layerNode.Next;
                        var layer = controller.layers[l];

                        LinkedList<string> states = new LinkedList<string>();
                        states.AddFirst("index");
                        states.AddRangeLast(AddStates(layer.stateMachine, layerNode.Value, ref tags));

                        IndentLevel++;
                        LinkedListNode<string> layerIndexNode = states.First;
                        if(states.Count > 1)
                            states.AddAfter(layerIndexNode, "");
                        states.AddRangeAfter(layerIndexNode, ToVariable(layerIndexNode.Value, l).Skip(1));
                        states.Remove(layerIndexNode);
                        IndentLevel--;

                        states.AddFirst($"{indent}{{");
                        states.AddLast($"{indent}}}");
                        states.AddLast("");

                        values.AddRangeAfter(layerNode, states);
                        values.AddAfter(layerNode, ToClass(layerNode.Value));
                        values.Remove(layerNode);
                    }
                    values.RemoveLast();
                    tags.Remove("");

                    // Add the tags
                    LinkedListNode<string> tagsNode = values.First.Next;
                    values.AddAfter(tagsNode, "");
                    values.AddAfter(tagsNode, $"{indent}}}");
                    IndentLevel++;
                    values.AddRangeAfter(tagsNode,
                        tags.ToIdentifiers(tagsNode.Value).
                        SelectMany((id, i) => ToVariable(id, tags.ElementAt(i)))
                        .Skip(1)
                    );
                    IndentLevel--;
                    values.AddAfter(tagsNode, $"{indent}{{");
                    values.AddAfter(tagsNode, ToClass(tagsNode.Value));
                    values.Remove(tagsNode);

                    // Add the parameters
                    LinkedListNode<string> parameterNode = values.First;
                    values.AddAfter(parameterNode, "");
                    values.AddAfter(parameterNode, $"{indent}}}");
                    IndentLevel++;
                    values.AddRangeAfter(parameterNode,
                        controller.parameters.Select(p => p.name).
                        ToIdentifiers(parameterNode.Value).
                        SelectMany((id, i) => ToVariable(id, controller.parameters[i].nameHash).Skip(1))
                    );
                    IndentLevel--;
                    values.AddAfter(parameterNode, $"{indent}{{");
                    values.AddAfter(parameterNode, ToClass(parameterNode.Value));
                    values.Remove(parameterNode);
                }
                IndentLevel--;

                // Add the values to the controller
                values.AddFirst($"{indent}{{");
                values.AddLast($"{indent}}}");
                values.AddLast("");

                body.AddRangeAfter(controllerNode, values);
                body.AddAfter(controllerNode, ToClass(controllerNode.Value));
                body.Remove(controllerNode);
            }
            body.RemoveLast();

            OverwriteOrAddFile(fileName, body, typeof(ControllersWriter));

            // Find all states and statemachines within a statemachine and add them to a list of states
            LinkedList<string> AddStates(AnimatorStateMachine stateMachine, string enclosingType, ref HashSet<string> tags)
            {
                LinkedList<string> states = new LinkedList<string>
                (
                    stateMachine.states.Select(s => s.state.name).
                    Concat(stateMachine.stateMachines.Select(sm => sm.stateMachine.name)).
                    ToIdentifiers(enclosingType)
                );

                IndentLevel++;

                for(int s = stateMachine.states.Length - 1; s >= 0; s--)
                {
                    LinkedListNode<string> stateNode = states.First;
                    for(int n = 0; n < s; n++)
                        stateNode = stateNode.Next;
                    var state = stateMachine.states[s].state;

                    tags.Add(state.tag);

                    states.AddRangeAfter(stateNode, ToVariable(stateNode.Value, state.nameHash).Skip(1));
                    states.Remove(stateNode);
                }

                for(int sm = stateMachine.stateMachines.Length - 1; sm >= 0; sm--)
                {
                    LinkedListNode<string> stateMachineNode = states.Last;
                    for(int n = 0; n < sm; n++)
                        stateMachineNode = stateMachineNode.Previous;
                    var childStateMachine = stateMachine.stateMachines[stateMachine.stateMachines.Length - 1 - sm].stateMachine;

                    states.AddAfter(stateMachineNode, $"{indent}}}");
                    states.AddRangeAfter(stateMachineNode, AddStates(childStateMachine, stateMachineNode.Value, ref tags));
                    states.AddAfter(stateMachineNode, $"{indent}{{");
                    states.AddAfter(stateMachineNode, ToClass(stateMachineNode.Value));
                    states.AddAfter(stateMachineNode, "");

                    states.Remove(stateMachineNode);
                }

                IndentLevel--;

                return states;
            }
        }
    }
}
