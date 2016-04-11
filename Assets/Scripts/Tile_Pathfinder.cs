using UnityEngine;
using System.Collections;

public class Tile_Pathfinder : MonoBehaviour {

    public bool isWall = false; //By default, it will be false.
    int id = 0;

    /// <summary>
    /// Function used to consider the tile as a Wall.
    /// </summary>
    public void setWall()
    { isWall = true; }

    /// <summary>
    /// Function used to consider the tile as a floor.
    /// </summary>
    public void setFloor()
    { isWall = false; }

    /// <summary>
    /// Function used to change the value of the presence of a wall.
    /// </summary>
    /// <param name="value">A boolean to define if it is a wall or not.</param>
    public void setValue(bool value)
    { isWall = value; }

    public void setID(int _id)
    { id = _id; }

    public int getID()
    { return id; }
}
