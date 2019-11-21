using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Sprites;
using System.IO;

public class SpritePlacer : EditorWindow
{
    static EditorWindow window;
    static Vector2 tileScrollPosition = Vector2.zero;

    public static Sprite[] spriteArray;
    public static Texture2D[] textureArray;
    static Sprite[] cmCurSprites;

    static bool makeCollider = false;

    static List<int> cmSelectedTile = new List<int>();
    static List<Sprite> spriteList = new List<Sprite>();
    static List<Sprite> cmCurSprite = new List<Sprite>();

    Texture2D aTexture;
    static GameObject headObject;
    static GameObject obj;
    int spriteUnit = 64;
    int sizeMultiplier = 80;

    int selectedTool;

    [MenuItem("Tools/SpritePlacer")]
    public static void OnEnable()
    {
        window = EditorWindow.GetWindow(typeof(SpritePlacer));
        window.minSize = new Vector2(200, 400);

        //spriteArray = Resources.LoadAll<Sprite>("Sprites");
        spriteArray = Resources.LoadAll<Sprite>("Sprites");
        textureArray = Resources.LoadAll<Texture2D>("Sprites");
        headObject = new GameObject("Sprite map");

        SceneView.duringSceneGui += DrawObjectInEditor;
    }

    public void OnGUI()
    {
        
        if(spriteArray == null)
        {
            return;
        }

        int columnCount = Mathf.RoundToInt((position.width) / 70) - 2;
        int x = 0;
        int y = 0;
        int current = 0;

        GUILayout.Label("2D Tile Placer", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();
        spriteUnit = EditorGUILayout.IntField("Sprite units: ", spriteUnit);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Paint With Collider", GUILayout.Width(150));
        makeCollider = EditorGUILayout.Toggle(makeCollider);
        EditorGUILayout.EndHorizontal();

        //GUILayout.BeginVertical(EditorStyles.helpBox);

        string[] tools = { "Draw", "Erase"};
        selectedTool = GUILayout.Toolbar(selectedTool, tools);

        tileScrollPosition = EditorGUILayout.BeginScrollView(tileScrollPosition, false, true, GUILayout.Width(position.width));
        GUILayout.BeginHorizontal();
        
        for (int i = 0; i < spriteArray.Length; i++)
        {
           
            Sprite child = spriteArray[i];
            Rect newRect = new Rect(child.rect.x / child.texture.width,
                                    child.rect.y / child.texture.height,
                                    child.rect.width / child.texture.width,
                                    child.rect.height / child.texture.height);

            aTexture = SpriteUtility.GetSpriteTexture(child, false);
            //

            if (GUILayout.Button("", GUILayout.Width(spriteUnit + (spriteUnit * (1/16))), GUILayout.Height(spriteUnit + (spriteUnit * (1 / 16)))))
            {
                //draw a clickable button
                if (cmSelectedTile != null && !Event.current.control)
                {
                    //empty the selected tile list if control isn't held. Allows multiselect of tiles.
                    cmSelectedTile.Clear();
                    cmCurSprite.Clear();
                }
                cmSelectedTile.Add(current); //Adds clicked on tile to list of selected tiles.
                cmCurSprite.Add(child);
            }

            GUI.DrawTextureWithTexCoords(new Rect(5 + (x * spriteUnit * 1.06f), 4 + (y * spriteUnit * 1.05f), spriteUnit, spriteUnit), child.texture, newRect, true);
            //GUI.DrawTexture(newRect, spriteArray[i].texture, ScaleMode.StretchToFill, GUILayout.Width(32.0f), GUILayout.Height(32));
            //GUILayout.Label(aTexture);
            if (x < columnCount)
            {
                x++;
            }
            else
            {
                // if we have enough columns to fill the scroll area, reset the column count and start a new line of buttons
                x = 0;
                y++;
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
            }
        }
        
        EditorGUILayout.EndScrollView();
        //GUILayout.EndVertical();
    }

    static void DrawObjectInEditor(SceneView sceneview)
    {
        Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
        {
            if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
            {
                Debug.Log(Event.current.mousePosition);

                Transform child = headObject.GetComponentInChildren<Transform>();

                obj = new GameObject("Sprite");
                obj.transform.parent = child.transform;
                obj.transform.position = hit.point;
                obj.AddComponent<SpriteRenderer>();

                obj.GetComponent<SpriteRenderer>().sprite = cmCurSprite[0];

                if (makeCollider)
                {
                    obj.AddComponent<BoxCollider2D>();
                }

                //Instantiate(obj, hit.point, Quaternion.identity);
            }
        }
    }
}
