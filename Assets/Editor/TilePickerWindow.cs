using UnityEngine;
using System.Collections;
using UnityEditor;

//Script used for Picking and placing the tiles in the Unity Editor. Won't affect the game.

public class TilePickerWindow : EditorWindow {

    public enum Scale //If it's still too small, add some zoom. x5 is too big for mine, so I guess it's ok for 1080p screens.
    {
        x1,
        x2,
        x3,
        x4,
        x5
    }

    Scale scale;

    private Vector2 currentSelection = Vector2.zero; //To know which tile you selected
    public Vector2 scrollPosition = Vector2.zero;
    
    //Floats for the highlight section
    public float highR = 0.9f;
    public float highG = 0.9f;
    public float highB = 0f;

    /// <summary>
    /// This function is called when you change the highlight color
    /// </summary>
    /// <param name="r">Red value</param>
    /// <param name="g">Green value</param>
    /// <param name="b">Blue value</param>
    private void setHighlightColor(float r, float g, float b)
    {
        highR = r;
        highG = g;
        highB = b;
    }


    /// <summary>
    /// This function is called when you click on "Tile Picker" in the Window Menu. It will display a new window with the tiles to pick.
    /// </summary>
    [MenuItem("Window/Tile Picker")]
    public static void OpenTilePickerWindow()
    {
        var window = EditorWindow.GetWindow(typeof(TilePickerWindow));
        var windowTitle = new GUIContent();
        windowTitle.text = "Tile Picker";
        window.titleContent = windowTitle;
    }

    void OnGUI()
    {
        try
        {
            if (Selection.activeObject == null) //If nothing is selected, it will stop the display and will show an empty window
                return;

            var selection = ((GameObject)Selection.activeObject).GetComponent<TileMap>();

            if (selection != null)
            {
                var texture2D = selection.texture2D;
                if (texture2D != null)
                {
                    //Scaling/zoom the texture (for big screens with high resolutions)
                    scale = (Scale)EditorGUILayout.EnumPopup("Zoom", scale);
                    var newScale = ((int)scale) + 1;
                    var newTextureSize = new Vector2(texture2D.width, texture2D.height) * newScale;
                    var offset = new Vector2(10, 25);

                    //The scrolling bar when you zoom too much. (because you like to enjoy these beautiful tiles)
                    var viewPort = new Rect(0, 0, position.width - 5, position.height - 5);
                    var contentSize = new Rect(0, 0, newTextureSize.x + offset.x, newTextureSize.y + offset.y);
                    scrollPosition = GUI.BeginScrollView(viewPort, scrollPosition, contentSize);

                    GUI.DrawTexture(new Rect(offset.x, offset.y, newTextureSize.x, newTextureSize.y), texture2D);

                    //Highlight of the selection section
                    var tile = selection.tileSize * newScale;
                    var grid = new Vector2(newTextureSize.x / tile.x, newTextureSize.y / tile.y);
                    var selectionPos = new Vector2(tile.x * currentSelection.x + offset.x, tile.y * currentSelection.y + offset.y);

                    var boxTex = new Texture2D(1, 1);
                    boxTex.SetPixel(0, 0, new Color(highR, highG, highB, 0.4f)); //If you want to change the highlight color, it's here.
                    boxTex.Apply();

                    var style = new GUIStyle(GUI.skin.customStyles[0]);
                    style.normal.background = boxTex;

                    GUI.Box(new Rect(selectionPos.x, selectionPos.y, tile.x, tile.y), "", style);

                    //Event handler to select the texture clicked + highlight it after.
                    var cEvent = Event.current;
                    Vector2 mousePos = new Vector2(cEvent.mousePosition.x, cEvent.mousePosition.y);

                    if (cEvent.type == EventType.mouseDown && cEvent.button == 0)
                    {
                        currentSelection.x = Mathf.Floor((mousePos.x + scrollPosition.x) / tile.x);
                        currentSelection.y = Mathf.Floor((mousePos.y + scrollPosition.y) / tile.y);

                        if (currentSelection.x > grid.x - 1)
                            currentSelection.x = grid.x - 1;

                        if (currentSelection.y > grid.y - 1)
                            currentSelection.y = grid.y - 1;

                        selection.tileID = (int)(currentSelection.x + (currentSelection.y * grid.x) + 1);

                        Repaint(); //Update window after click
                    }

                    GUI.EndScrollView();

                }
            }
        }
        catch (System.Exception)
        {
            //Will catch all caution and warning errors when you click in Tile picker window when nothing appears.
        }

    }
}
