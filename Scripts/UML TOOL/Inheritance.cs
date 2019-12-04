using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
                if (!lines[i].ChildNode.Minalized)
                    beginPosition = new Vector3(lines[i].ChildNode.MaxNodeSize.x + (lines[i].ChildNode.MaxNodeSize.width * .5f), (lines[i].ChildNode.MaxNodeSize.y + lines[i].ChildNode.MaxNodeSize.height * .5f));
                else
                    beginPosition = new Vector3(lines[i].ChildNode.MinNodeSize.x + (lines[i].ChildNode.MinNodeSize.width * .5f), (lines[i].ChildNode.MinNodeSize.y + lines[i].ChildNode.MinNodeSize.height * .5f));
                //check if the parentnode is minalized
                if (!lines[i].ParentNode.Minalized)
                    endPosition = new Vector3(lines[i].ParentNode.MaxNodeSize.x + (lines[i].ParentNode.MaxNodeSize.width * .5f), lines[i].ParentNode.MaxNodeSize.y + (lines[i].ParentNode.MaxNodeSize.height * .5f));
                else
                    endPosition = new Vector3(lines[i].ParentNode.MinNodeSize.x + (lines[i].ParentNode.MinNodeSize.width * .5f), lines[i].ParentNode.MinNodeSize.y + (lines[i].ParentNode.MinNodeSize.height * .5f));

                //drawing angle of the 2 vectors
                Vector2 delta = beginPosition - endPosition;
                Vector2 arrowPosition = beginPosition + -delta / 2;
                delta.Normalize();
                float angle = Mathf.Rad2Deg * Mathf.Acos(delta.x);
                if (beginPosition.y < endPosition.y)
                    angle *= -1;

                //Calculate Arrow
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
            for (int i = 0; i < node.Count; i++)
                if (node[i].Instance == null)
                    node.RemoveAt(i);

            lines = new List<SetLines>();
            for (int i = 0; i < node.Count; i++)
                if (node[i].NodeInfo.Parent != string.Empty)
                    for (int j = 0; j < node.Count; j++)
                        if (node[i].NodeInfo.Parent == node[j].NodeInfo.ClassName)
                            lines.Add(new SetLines(node[j], node[i]));

        }
    }
    public struct SetLines
    {
        public Node ChildNode;
        public Node ParentNode;
        public SetLines(Node childNode,Node parentNode)
        {
            ChildNode = childNode;
            ParentNode = parentNode;
        }
    }
}
