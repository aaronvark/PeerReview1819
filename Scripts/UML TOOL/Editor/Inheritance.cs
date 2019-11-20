using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace UnityEngine.Scripting.UML
{
    public class Inheritance
    {
        //Child Parent
        private Dictionary<Node, Node> inheritance = new Dictionary<Node, Node>();
        private List<SetLines> lines = new List<SetLines>();

        public void DrawInheritances()
        {

            Handles.color = Color.black;
            for (int i = 0; i < lines.Count; i++)
            {
                Vector2 beginPosition;
                Vector2 endPosition;

                //Checking if the childnode is minalized
                if (!lines[i].ChildNode.minalized)
                    beginPosition = new Vector3(lines[i].ChildNode.maxNodeSize.x + (lines[i].ChildNode.maxNodeSize.width * .5f), (lines[i].ChildNode.maxNodeSize.y + lines[i].ChildNode.maxNodeSize.height * .5f));
                else
                    beginPosition = new Vector3(lines[i].ChildNode.minNodeSize.x + (lines[i].ChildNode.minNodeSize.width * .5f), (lines[i].ChildNode.minNodeSize.y + lines[i].ChildNode.minNodeSize.height * .5f));
                //check if the parentnode is minalized
                if (!lines[i].parentNode.minalized)
                    endPosition = new Vector3(lines[i].parentNode.maxNodeSize.x + (lines[i].parentNode.maxNodeSize.width * .5f), lines[i].parentNode.maxNodeSize.y + (lines[i].parentNode.maxNodeSize.height * .5f));
                else
                    endPosition = new Vector3(lines[i].parentNode.minNodeSize.x + (lines[i].parentNode.minNodeSize.width * .5f), lines[i].parentNode.minNodeSize.y + (lines[i].parentNode.minNodeSize.height * .5f));

                //drawing angle of the 2 vectors
                Vector2 delta = beginPosition - endPosition;
                Vector2 arrowPosition = beginPosition + -delta / 2;
                delta.Normalize();
                float angle = Mathf.Rad2Deg * Mathf.Acos(delta.x);
                if (beginPosition.y < endPosition.y)
                    angle *= -1;
                //Vector2 arrowPosition = new Vector3(dictionary.Key.maxNodeSize.x + (dictionary.Key.maxNodeSize.width), dictionary.Key.maxNodeSize.y + (dictionary.Key.maxNodeSize.height));


                Vector2 newPos = CalculateArrow(angle, false);
                Handles.DrawLine(arrowPosition + -newPos, arrowPosition);
                Vector2 newPos2 = CalculateArrow(angle, true);
                Handles.DrawLine(arrowPosition + -newPos2, arrowPosition);
                Handles.DrawLine(arrowPosition - newPos, arrowPosition - newPos2);

                Handles.DrawLine(beginPosition, endPosition);
            }
        }

        /// <summary>
        /// Calculate the arrow
        /// <returns></returns>
        private Vector2 CalculateArrow(float angle, bool negative)
        {
            if (negative)
                angle += 20;
            else
                angle -= 20;
            Vector2 newPos = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle));
            newPos *= 25;

            return newPos;
        }

        public void SetInheritance(List<Node> node)
        {
            lines = new List<SetLines>();
            for (int i = 0; i < node.Count; i++)
                if (node[i].nodeInfo.Parent != string.Empty)
                    for (int j = 0; j < node.Count; j++)
                        if (node[i].nodeInfo.Parent == node[j].nodeInfo.ClassName)
                            lines.Add(new SetLines(node[j], node[i]));
                                //inheritance.Add(node[j], node[i]);

        }
    }
    public struct SetLines
    {
        public Node ChildNode;
        public Node parentNode;
        public SetLines(Node child,Node Parent)
        {
            ChildNode = child;
            parentNode = Parent;
        }
    }
}
