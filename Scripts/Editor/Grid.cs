using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UnityEngine.Scripting.UML
{
    public class Grid
    {
        //grid variables
        private Vector2 offset;
        private Vector2 drag;
        private EditorWindow window;


        public Grid(EditorWindow window)
        {
            this.window = window;
        }

        public void UpdateGrid()
        {
            DrawGrid(20, 0.2f, Color.black);
            DrawGrid(100, 0.4f, Color.black);
        }

        //Drawgrid source http://gram.gs/gramlog/creating-node-based-edit
        //Drawing the grid in the editor.
        private void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor)
        {
            int widthDivs = Mathf.CeilToInt(window.position.width / gridSpacing);
            int heightDivs = Mathf.CeilToInt(window.position.height / gridSpacing);

            Handles.BeginGUI();
            Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

            offset += drag * 0.5f;
            Vector3 newOffset = new Vector3(offset.x % gridSpacing, offset.y % gridSpacing, 0);

            for (int i = 0; i < widthDivs; i++)
                Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset, new Vector3(gridSpacing * i, window.position.height, 0f) + newOffset);

            for (int j = 0; j < heightDivs; j++)
                Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset, new Vector3(window.position.width, gridSpacing * j, 0f) + newOffset);

            Handles.color = Color.white;
            Handles.EndGUI();
        }
    }
}