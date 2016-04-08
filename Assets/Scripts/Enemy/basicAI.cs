using System;
using UnityEngine;
using System.Collections;


public class basicAI : MonoBehaviour {

   
    public enum State
    {
        PATROL,
        CHASE
    }

    public State state;
    private bool isAlive;

    //Patrol variables
    public GameObject[] waypoints;
    Vector3[] path;
    private int waypointIndex = 0;
    private int pathIndex = 0;
    public float patrolSpeed = 4.0f;

    //Chase variables
    public float chaseSpeed = 5.5f;
    public GameObject target;

    //Variables for pathfinding
    public GameObject TileMap;
    GameObject[] TilesContainer;

    //Called before initialization, to instialize the path finding before using it.
    void Awake()
    {
        TilesContainer = new GameObject[Mathf.RoundToInt(TileMap.GetComponent<TileMap>().mapSize.x) * Mathf.RoundToInt(TileMap.GetComponent<TileMap>().mapSize.y)];
        print("I got " + TilesContainer.Length + " tiles");

        print(TilesContainer.Length + "==" + TileMap.transform.GetChild(0).transform.childCount);
        if (TilesContainer.Length == TileMap.transform.GetChild(0).transform.childCount)
        {
            //We are sorting the tiles in alphabetical order here
            var tilenumber = 0;
            var NamesContainer = new String[TilesContainer.Length];
            foreach (Transform child in TileMap.transform.GetChild(0).transform)
                NamesContainer[tilenumber++] = child.gameObject.name;

            Array.Sort(NamesContainer);
            for (var i = 0; i < TilesContainer.Length; ++i)
            {
                TilesContainer[i] = GameObject.Find(TileMap.name + "/Tiles/" + NamesContainer[i]);
                TilesContainer[i].GetComponent<Tile_Pathfinder>().setID(i);
            }

        }
        else
            print("Grid isn't full."); //TODO Add case if we don't have all the grid

        updatePathfinding(waypoints[waypointIndex]);
    }

	// Use this for initialization
	void Start () {

        state = basicAI.State.PATROL;

        isAlive = true;

        //Beginning of Final State Machine.
        StartCoroutine("FSM");
	}

    IEnumerator FSM()
    {
        while (isAlive)
        {
            switch (state)
            {
                case State.PATROL:
                    Patrol();
                    break;

                case State.CHASE:
                    Chase();
                    break;

                default:
                    Patrol();
                    break;
            }
            yield return null;
        }
    }

    /// <summary>
    /// Function for the PATROL state. Will go through the waypoints placed in Editor.
    /// </summary>
    void Patrol()
    {
        if (Vector3.Distance(gameObject.transform.position, path[pathIndex]) >= .05) //If he is too far from a waypoint
            Move(path[pathIndex]);
        else if (Vector3.Distance(gameObject.transform.position, path[pathIndex]) < .05) //If he is too close from a waypoint
        {
            pathIndex++;
            if (pathIndex >= path.Length) //If it's longer, we have to go the next waypoint.
            {
                pathIndex = 0; //Reseting the path index
                waypointIndex = ++waypointIndex % waypoints.Length;
                updatePathfinding(waypoints[waypointIndex]);
            }
        }
        else //Else, he won't move.
            Move(Vector3.zero);
    }

    /// <summary>
    /// Function for the CHASE state. Will purchase the player and (try to) shoot him.
    /// </summary>
    void Chase()
    {
        Move(target.transform.position);
        //TODO Add the pew pew pew gun
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            state = State.CHASE;
            target = other.gameObject;
        }
    }

    /// <summary>
    /// Function used to move the AI.
    /// </summary>
    /// <param name="move">Position of the target.</param>
    public void Move(Vector3 target)
    {
        var direction = target - gameObject.transform.position;
        var speed = 1.0f;

        if (state == State.PATROL)
            speed = patrolSpeed;
        else if (state == State.CHASE)
            speed = chaseSpeed;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));
        gameObject.GetComponent<Rigidbody>().velocity = direction.normalized * speed;

        //If the AI is too fast, we reduce his speed here.
        if (gameObject.GetComponent<Rigidbody>().velocity.sqrMagnitude > Mathf.Pow(speed, 2))
        {
            var coef = (Mathf.Abs(gameObject.GetComponent<Rigidbody>().velocity.x) + Mathf.Abs(gameObject.GetComponent<Rigidbody>().velocity.y)) / speed;
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(gameObject.GetComponent<Rigidbody>().velocity.x / coef, gameObject.GetComponent<Rigidbody>().velocity.y / coef, 0);
        }
    }

    void updatePathfinding(GameObject goal)
    {
        var x = Mathf.RoundToInt(TileMap.GetComponent<TileMap>().mapSize.x);
        var y = Mathf.RoundToInt(TileMap.GetComponent<TileMap>().mapSize.y);
        int[,] map = new int[y, x]; //First element is the number of lines, second element is the columns (Line, row)

        for (var i = 0; i < map.Length; ++i)
            map[i / x, i % x] = Convert.ToInt32(TilesContainer[i].GetComponent<Tile_Pathfinder>().isWall);

        var graph = new Graph(map);

        var search = new Search(graph);
        search.Start(graph.nodes[getClosestTile(gameObject)], graph.nodes[getClosestTile(goal)]);

        while (!search.finished)
            search.Step();


        var tempV3 = new Vector3[search.path.Count];
        var count = 0;

        //print("The next following elements will be the path found.");
        foreach (var node in search.path)
            tempV3[count++] = TilesContainer[Int32.Parse(node.label)].transform.position;

        path = new Vector3[tempV3.Length];
        path = tempV3;

    }

    void sortArray(GameObject[] array)
    {
        GameObject[] temp = array;

        var isSorted = false;

        while (! isSorted)
        {
            isSorted = true;
            for (var i = 0; i < temp.Length - 1; ++i)
            {
                //if (String.Compare(temp[i].name, temp[i+1].name))
            }
        }
        
    }

    /// <summary>
    /// This function is used to get the closest from a given object.
    /// </summary>
    /// <param name="obj">Gameobject that we want to have the closest tile.</param>
    /// <returns>Returns the ID of the tile.</returns>
    int getClosestTile(GameObject obj)
    {
        var colliders = Physics.OverlapSphere(obj.transform.position, 2f);
        var closestTile = new Collider();

        foreach (Collider hit in colliders)
        {
            if (hit != obj.transform.GetComponent<Collider>() && !closestTile && (hit.gameObject.GetComponent<Tile_Pathfinder>() != null)) //If no one is set and it's not the enemy itself.
                closestTile = hit;

            if (closestTile != null)
            {
                if (Vector3.Distance(obj.transform.position, hit.transform.position) <= Vector3.Distance(obj.transform.position, closestTile.transform.position) && (hit.gameObject.GetComponent<Tile_Pathfinder>() != null))
                    closestTile = hit;
            }
        }
        return closestTile.gameObject.GetComponent<Tile_Pathfinder>().getID();
    }
}
