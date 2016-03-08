using UnityEngine;
using System.Collections;
using UnityEditor; //To Edit Unity Editor, won't affect the game

//Script for creating the tilemap object in Unity directly

public class NewTileMap_Menu {

    [MenuItem("GameObject/Tile Map")]
    public static void CreateTileMap()
    {
        Debug.Log("Creation of a new tile map");
        GameObject tm = new GameObject("Tile map"); //Adding tile map in elements
        tm.AddComponent<TileMap>(); //Adding the TileMap script (from scripts folder)
    }
}
