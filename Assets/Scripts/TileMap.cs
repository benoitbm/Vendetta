using UnityEngine;
using System.Collections;

//Script used for creating and editing the tilemaps. Based on the tutorial Building a tilemap from Lynda.

public class TileMap : MonoBehaviour {

    public Vector2 mapSize = new Vector2(200, 100); //Number of tiles for the map 
    public Vector2 tileSize = new Vector2();
    public Vector2 gridSize = new Vector2();
    public Object[] spriteReferences; //Will contain informations about placed elements
    public Texture2D texture2D;
    public int pixelsToUnits = 100;
    public int tileID = 0;

    public GameObject tiles; //Will content all the tiles

    public Sprite currentTileBrush
    {
        get { return spriteReferences[tileID] as Sprite; }
    }
    

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDrawGizmosSelected()
    {
        var pos = transform.position;

        if (texture2D != null)
        {
            //Elements to draw the grid of the tile map
            Gizmos.color = Color.grey;
            var row = 0; //Used to build the grid
            var maxCol = mapSize.x; //Number of max columns
            var totalGrids = mapSize.x * mapSize.y;
            var tile = new Vector3(tileSize.x / pixelsToUnits, tileSize.y / pixelsToUnits);
            var offset = new Vector2(tile.x / 2, tile.y / 2);

            for (var i = 0; i < totalGrids; ++i)
            {
                var col = i % maxCol; // To know in which column we are. S

                var newX = (col * tile.x) + offset.x + pos.x;
                var newY = -(row * tile.y) - offset.y + pos.y; //Negative because Y is going to the down

                Gizmos.DrawWireCube(new Vector2(newX, newY), tile);

                if (col == maxCol -1) // To pass to the next row when we finish a row
                    ++row;
            }


            //Elements to draw the border of the tile map
            Gizmos.color = Color.white;
            var centerX = pos.x + (gridSize.x / 2);
            var centerY = pos.y - (gridSize.y / 2);

            Gizmos.DrawWireCube(new Vector2(centerX, centerY), gridSize); //Drawing the grid with this
        }
    }
}
