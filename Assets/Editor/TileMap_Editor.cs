using UnityEngine;
using System.Collections;
using UnityEditor;

//Script for editing Tilemap in Unity. Won't affect the game.

[CustomEditor(typeof(TileMap))]
public class TileMap_Editor : Editor {

    public TileMap tmap; //For the tilemap created before

    TileBrush brush;
    Vector3 mouseHitPos;

    bool mouseOnMap
    {
        get { return mouseHitPos.x > 0 && mouseHitPos.x < tmap.gridSize.x && mouseHitPos.y < 0 && mouseHitPos.y > -tmap.gridSize.y; }
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();

        var oldSize = tmap.mapSize;
        tmap.mapSize = EditorGUILayout.Vector2Field("Map Size:", tmap.mapSize);
        if (tmap.mapSize != oldSize) //We control here if the values changed to redraw the grid directly (and make it more dynamic)
            updateCalculations();

        var oldTexture = tmap.texture2D;
        tmap.texture2D = (Texture2D)EditorGUILayout.ObjectField("Tile map texture:", tmap.texture2D, typeof(Texture2D), false);

        if (oldTexture != tmap.texture2D)
        {
            updateCalculations();
            tmap.tileID = 1;
            createBrush();
        }

        if (tmap.texture2D == null) //Checking that a texture is chosen, if not...
            EditorGUILayout.HelpBox("A Texture 2D is not selected. Please select one.", MessageType.Warning); //It will show a message inside the editor

        else //Else, it will display some informations 
        {
            EditorGUILayout.LabelField("Tile Size :", tmap.tileSize.x + "x" + tmap.tileSize.y);
            EditorGUILayout.LabelField("Grid Size in Units :", tmap.gridSize.x + "x" + tmap.gridSize.y);
            EditorGUILayout.LabelField("Pixels to Units :", tmap.pixelsToUnits.ToString());
            updateBrush(tmap.currentTileBrush);

            if(GUILayout.Button("Clear map tile"))
            {
                if (EditorUtility.DisplayDialog("Clearing map's tiles ?", "Are you sure you want to clear the map's tiles ?\n /!\\ ALL DATA WILL BE LOST FOREVER /!\\", "Yes, clear the map", "No don't clear"))
                    clearMap();
            }
        }

        EditorGUILayout.EndVertical();
    }

    //Called when is displayed in Unity
    void OnEnable()
    {
        tmap = target as TileMap;
        Tools.current = Tool.View; //To be sure that we move elements (and not resize them, rotate or another)

        if (tmap.tiles == null)
        {
            var go = new GameObject("Tiles");
            go.transform.SetParent(tmap.transform);
            go.transform.position = Vector3.zero;

            tmap.tiles = go;
        }


        if (tmap.texture2D != null)
        {
            updateCalculations();
            NewBrush();
        }
    }

    //And this is called when you deselect the element
    void OnDisable()
    {
        DestroyBrush();
    }

    void OnSceneGUI()
    {
        if (brush != null)
        {
            updateHitPosition();
            MoveBrush();

            if (tmap.texture2D != null && mouseOnMap)
            {
                Event current = Event.current;
                if (current.shift) //Shift key to draw an element. If you want to change it, it's here
                    Draw(false);
                else if (current.control) //Control key to draw an element too. But it will set the tile as a wall value, which the AI won't go there.
                    Draw(true);
                else if (current.alt) //Alt / Option (if mac) key to remove a tile. Same as draw.
                    removeTile();            

            }
        }
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
        tmap.pixelsToUnits = Mathf.RoundToInt(sprite.rect.width / sprite.bounds.size.x);
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
            brush.sRenderer.sortingOrder = 1000;

            var PtU = tmap.pixelsToUnits;
            brush.brushsize = new Vector2(sprite.textureRect.width / PtU, sprite.textureRect.height / PtU);
            brush.UpdateBrush(sprite);
        }

    }

    void NewBrush()
    {
        if (brush == null)
            createBrush();
    }

    void DestroyBrush()
    {
        if (brush != null)
            DestroyImmediate(brush.gameObject);
    }

    public void updateBrush(Sprite sprite)
    {
        if (brush != null)
            brush.UpdateBrush(sprite);
    }

    void updateHitPosition()
    {
        var p = new Plane(tmap.transform.TransformDirection(Vector3.forward), Vector3.zero);
        var ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        var hit = Vector3.zero;
        var dist = 0f;

        if (p.Raycast(ray, out dist))
            hit = ray.origin + ray.direction.normalized * dist;

        mouseHitPos = tmap.transform.InverseTransformPoint(hit);
    }

    void MoveBrush()
    {
        var tileSize = tmap.tileSize.x / tmap.pixelsToUnits;

        var x = Mathf.Floor(mouseHitPos.x / tileSize) * tileSize;
        var y = Mathf.Floor(mouseHitPos.y / tileSize) * tileSize;

        var row = x / tileSize;
        var col = Mathf.Abs(y / tileSize) - 1;

        if (!mouseOnMap)
            return;

        var id = Mathf.RoundToInt((col * tmap.mapSize.x) + row);

        brush.tileID = id;

        x += tmap.transform.position.x + tileSize / 2;
        y += tmap.transform.position.y + tileSize / 2;

        brush.transform.position = new Vector3(x, y, tmap.transform.position.z);
    }

    void Draw()
    {
        var numberOfDigit = Mathf.FloorToInt(Mathf.Log10(tmap.mapSize.x * tmap.mapSize.y) + 1);

        var id = brush.tileID.ToString();

        if (id.Length < numberOfDigit)
            id = id.PadLeft(numberOfDigit, '0');

        var posX = brush.transform.position.x;
        var posY = brush.transform.position.y;

        GameObject tile = GameObject.Find(tmap.name + "/Tiles/tile_" + id);

        if (tile == null)
        {
            tile = new GameObject("tile_" + id);
            tile.transform.SetParent(tmap.tiles.transform);
            tile.transform.position = new Vector3(posX, posY, 0);
            tile.AddComponent<SpriteRenderer>();
            tile.AddComponent<Tile_Pathfinder>();

            tile.AddComponent<BoxCollider>();
            tile.GetComponent<BoxCollider>().isTrigger = true; //To avoid collisions between dynamic elements. Used only for pathfinding.
            tile.GetComponent<BoxCollider>().size = new Vector3(1.28f, 1.28f, 1.28f);

            tile.tag = "Bullet_through";
        }

        tile.GetComponent<SpriteRenderer>().sprite = brush.sRenderer.sprite;

    }

    void Draw(bool isWall)
    {
        var id = brush.tileID.ToString();

        var numberOfDigit = Mathf.FloorToInt(Mathf.Log10(tmap.mapSize.x * tmap.mapSize.y) + 1);

        if (id.Length < numberOfDigit) //We're checking if it has the same amount of digit as the max digit
            id = id.PadLeft(numberOfDigit, '0'); //If not, we're adding 0 before, to help for the sort later.

        var posX = brush.transform.position.x;
        var posY = brush.transform.position.y;
        GameObject tile = GameObject.Find(tmap.name + "/Tiles/tile_" + id);

        if (tile == null)
        {
            tile = new GameObject("tile_" + id);
            tile.transform.SetParent(tmap.tiles.transform);
            tile.transform.position = new Vector3(posX, posY, 0);
            tile.AddComponent<SpriteRenderer>();
            tile.AddComponent<Tile_Pathfinder>();

            tile.AddComponent<BoxCollider>();
            tile.GetComponent<BoxCollider>().isTrigger = true;
            tile.GetComponent<BoxCollider>().size = new Vector3(1.28f,1.28f,1.28f);

            tile.tag = "Bullet_through";
        }
        tile.GetComponent<Tile_Pathfinder>().setValue(isWall);
        tile.GetComponent<SpriteRenderer>().sprite = brush.sRenderer.sprite;

    }

    void removeTile()
    {
        var id = brush.tileID.ToString();

        var numberOfDigit = Mathf.FloorToInt(Mathf.Log10(tmap.mapSize.x * tmap.mapSize.y) + 1);

        if (id.Length < numberOfDigit) //We're checking if it has the same amount of digit as the max digit
            id = id.PadLeft(numberOfDigit, '0'); //If not, we're adding 0 before, to help for the sort later.

        GameObject tile = GameObject.Find(tmap.name + "/Tiles/tile_" + id);

        if (tile != null)
            DestroyImmediate(tile);
    }

    void clearMap()
    {
        for (var i = 0; i < tmap.tiles.transform.childCount; ++i)
        {
            Transform t = tmap.tiles.transform.GetChild(i);
            DestroyImmediate(t.gameObject);
            --i; //We remove one because the maximum decreased

        }
    }

}
