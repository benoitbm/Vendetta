using UnityEngine;
using System.Collections;

public class TileMap : MonoBehaviour {

    public Vector2 mapSize = new Vector2(200, 100); //Number of tiles for the map 
    public Texture2D texture2D;
    public Vector2 tileSize = new Vector2();
    public Object[] spriteReferences; //Will contain informations about placed elements

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
