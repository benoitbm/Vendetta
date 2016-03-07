using UnityEngine;
using System.Collections;
using UnityEditor;

//Script for editing Tilemap in Unity. Won't affect the game.

[CustomEditor(typeof(TileMap))]
public class TileMap_Editor : Editor {

    public TileMap tmap; //For the tilemap created before

    TileBrush brush;

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();

        var oldSize = tmap.mapSize;
        tmap.mapSize = EditorGUILayout.Vector2Field("Map Size:", tmap.mapSize);
        if (tmap.mapSize != oldSize) //We control here if the values changed to redraw the grid directly (and make it more dynamic)
            updateCalculations();

        tmap.texture2D = (Texture2D)EditorGUILayout.ObjectField("Tile map texture:", tmap.texture2D, typeof(Texture2D), false);

        if (tmap.texture2D == null) //Checking that a texture is chosen, if not...
            EditorGUILayout.HelpBox("A Texture 2D is not selected. Please select one.", MessageType.Warning); //It will show a message inside the editor

        else //Else, it will display some informations 
        {
            EditorGUILayout.LabelField("Tile Size :", tmap.tileSize.x+"x"+tmap.tileSize.y);
            EditorGUILayout.LabelField("Grid Size in Units :", tmap.gridSize.x + "x" + tmap.gridSize.y);
            EditorGUILayout.LabelField("Pixels to Units :", tmap.pixelsToUnits.ToString());
        }

        EditorGUILayout.EndVertical();
    }

    //Called when is displayed in Unity
    void OnEnable()
    {
        tmap = target as TileMap;
        Tools.current = Tool.View; //To be sure that we move elements (and not resize them, rotate or another)

        if (tmap.texture2D != null)
            updateCalculations();

    }

    /// <summary>
    /// This function is used to calcul the grid informations for the Unity Editor.
    /// </summary>
    private void updateCalculations()
    {
        var path = AssetDatabase.GetAssetPath(tmap.texture2D);
        tmap.spriteReferences = AssetDatabase.LoadAllAssetsAtPath(path);

        var sprite = (Sprite)tmap.spriteReferences[1]; //We take the 2nd value because the 1st one is a reference
        var w = sprite.textureRect.width;
        var h = sprite.textureRect.height;

        tmap.tileSize = new Vector2(w, h); //Updating the tile size in the file
        tmap.pixelsToUnits = (int)(sprite.rect.width / sprite.bounds.size.x);
        tmap.gridSize = new Vector2((w / tmap.pixelsToUnits) * tmap.mapSize.x, (h / tmap.pixelsToUnits) * tmap.mapSize.y);
    }

    void createBrush()
    {
        var sprite = tmap.currentTileBrush;
        
        if (sprite != null)
        {
            GameObject go = new GameObject("Brush");
            go.transform.SetParent(tmap.transform);

            brush = go.AddComponent<TileBrush>();
            brush.sRenderer = go.AddComponent<SpriteRenderer>();

            var PtU = tmap.pixelsToUnits;
            brush.brushsize = new Vector2(sprite.textureRect.width / PtU, sprite.textureRect.height / PtU);
            brush.UpdateBrush(sprite);
        }

    }
}
