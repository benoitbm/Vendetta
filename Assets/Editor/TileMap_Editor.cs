using UnityEngine;
using System.Collections;
using UnityEditor;

//Script for editing Tilemap in Unity. Won't affect the game.

[CustomEditor(typeof(TileMap))]
public class TileMap_Editor : Editor {

    public TileMap tmap; //For the tilemap created before

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();
        tmap.mapSize = EditorGUILayout.Vector2Field("Map Size:", tmap.mapSize);
        tmap.texture2D = (Texture2D)EditorGUILayout.ObjectField("Tile map texture:", tmap.texture2D, typeof(Texture2D), false);

        if (tmap.texture2D == null) //Checking that a texture is chosen, if not...
        {
            //It will show a message inside the editor
            EditorGUILayout.HelpBox("A Texture 2D is not selected. Please select one.", MessageType.Warning);   
        }
        else
        {
            EditorGUILayout.LabelField("Tile Size :", tmap.tileSize.x+"x"+tmap.tileSize.y);
        }

        EditorGUILayout.EndVertical();
    }

    //Called when is displayed in Unity
    void OnEnable()
    {
        tmap = target as TileMap;
        Tools.current = Tool.View; //To be sure that we move elements (and not resize them, rotate or another)

        if (tmap.texture2D != null)
        {
            var path = AssetDatabase.GetAssetPath(tmap.texture2D);
            tmap.spriteReferences = AssetDatabase.LoadAllAssetsAtPath(path);

            var sprite = (Sprite)tmap.spriteReferences[1]; //We take the 2nd value because the 1st one is a reference
            var w = sprite.textureRect.width;
            var h = sprite.textureRect.height;

            tmap.tileSize = new Vector2(w, h); //Updating the tile size in the file
        }

    }
}
