using System;
using UnityEngine;
using System.Collections;

//Script holding the Basic AI for the game, as for Enemies and Civilians.

public class basicAI : MonoBehaviour {

    public enum State
    {
        PATROL,
        CHASE,
        INSPECT
    }

    public State state;
    private bool isAlive;

    //Patrol variables
    public bool randomPatrol = false;

    public GameObject[] waypoints;
    Vector3[] path;

    private int waypointIndex = 0;
    private int pathIndex = 0;

    public float patrolSpeed = 4.0f;

    //Chase variables
    public float chaseSpeed = 5.5f;
    public float distanceFromTarget = 5; //Distance between the enemy and the player during the chase. Should be higher than 2.
    public GameObject target;
    Vector3 lastPosition;
    bool playerVisible = true;

    //Inspect variables
    bool reachedDestination = false;
    public float inspectSpeed = 4.5f;

    //Variables for pathfinding
    public GameObject TileMap;
    GameObject[] TilesContainer;

    //Called before initialization, to instialize the path finding before using it.
    void Awake()
    {
        TilesContainer = new GameObject[Mathf.RoundToInt(TileMap.GetComponent<TileMap>().mapSize.x) * Mathf.RoundToInt(TileMap.GetComponent<TileMap>().mapSize.y)];

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
        {
            Debug.LogError("Grid isn't full."); //TODO Add case if we don't have all the grid
            //UnityEditor.EditorApplication.isPlaying = false; //For the moment, it will stop the editor. Later it could add the empty grids.
        }

        if (randomPatrol)
        {
            waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
            waypointIndex = UnityEngine.Random.Range(0, waypoints.Length - 1);
        }

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

                case State.INSPECT:
                    Inspect();
                    break;
                
                default:
                    Patrol();
                    break;
            }
            yield return null;
        }
    }

    /// <summary>
    /// Function for the PATROL state. Will go through the waypoints placed in Editor or randomly through avaible waypoints.
    /// </summary>
    void Patrol()
    {
        gameObject.GetComponent<Rigidbody>().ResetInertiaTensor();

        if (pathIndex == 1 && path.Length == 1)
            pathIndex = 0;

        if (Vector3.Distance(gameObject.transform.position, path[pathIndex]) >= .05) //If he is too far from a waypoint
            Move(path[pathIndex]);
        else if (Vector3.Distance(gameObject.transform.position, path[pathIndex]) < .05) //If he is too close from a waypoint
        {
            pathIndex++;
            if (pathIndex >= path.Length) //If it's longer, we have to go the next waypoint.
            {
                pathIndex = 1; //Resetting the path index to 1 (avoiding to go back on currect tile).

                if (randomPatrol)
                {
                    var tempIndex = UnityEngine.Random.Range(0, waypoints.Length-1);

                    while (waypointIndex == tempIndex) //To be sure that he won't go again on the same waypoint.
                        tempIndex = UnityEngine.Random.Range(0, waypoints.Length-1);

                    waypointIndex = tempIndex;
                }
                else
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
        if (target != null)
        {
            //Moving to target
            RaycastHit hit;
            var ray = new Ray(transform.position, -Vector3.Normalize(transform.position - target.transform.position));
            Debug.DrawRay(transform.position, -(transform.position - target.transform.position));
            if (Physics.Raycast(ray, out hit, 20f))
            {
                if (hit.collider && hit.collider.tag == "Player")
                {
                    gameObject.transform.GetComponentInChildren<GunScript>().enemyShot();
                    playerVisible = true;
                }
                else if (!(hit.collider.tag == "Bullet" || hit.collider.tag == "Bullet_through" || hit.collider.tag == "Waypoint"))
                {
                    if (playerVisible)
                    {
                        playerVisible = false;
                        lastPosition = target.transform.position;
                    }
                }
            }

            if (playerVisible)
            {
                if (Vector3.Distance(gameObject.transform.position, target.transform.position) >= distanceFromTarget)
                    Move(target.transform.position);
                else //If it's too close, it won't move.
                {
                    var direction = target.transform.position - gameObject.transform.position;
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));
                    gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }
            }
            else
            {
                var temp = new GameObject("lastPos");
                temp.transform.position = lastPosition;
                updatePathfinding(temp);
                DestroyImmediate(temp);

                pathIndex = 1;
                state = State.INSPECT;
            }
        }
        else
        {
            state = State.PATROL;
            target = null;

            //Resetting pathfinding
            pathIndex = 0;
            updatePathfinding(waypoints[waypointIndex]);
        }
    }

    void Inspect()
    {
        gameObject.GetComponent<Rigidbody>().ResetInertiaTensor();

        if (reachedDestination)
        {
            pathIndex = 1;
            updatePathfinding(waypoints[waypointIndex]);
            reachedDestination = false;
            state = State.PATROL;
        }
        else
        {
            if (pathIndex == 1 && path.Length == 1)
                pathIndex = 0;

            if (Vector3.Distance(gameObject.transform.position, path[pathIndex]) >= .05) //If he is too far from a waypoint
                Move(path[pathIndex]);
            else if (Vector3.Distance(gameObject.transform.position, path[pathIndex]) < .05) //If he is too close from a waypoint
            {
                pathIndex++;
                reachedDestination = (pathIndex >= path.Length);
            }
        }
    }

    /// <summary>
    /// Function used by the children to send with what they collide.
    /// </summary>
    /// <param name="other">The collision the children detect.</param>
    public void onCollision(Collider other)
    {
        if (other.tag == "Player")
        {
            RaycastHit hit;
            var ray = new Ray(transform.position, -Vector3.Normalize(transform.position - other.transform.position));
            Debug.DrawRay(transform.position, -(transform.position - other.transform.position)); //We're creating a raycast to detect if there is a obstacle
            if (Physics.Raycast(ray, out hit, 15f))
            {
                if (hit.collider && hit.collider.tag == "Player") //If we got the player, it means there is no obstacle which hides the player, and then we go through the chase state
                {
                    state = State.CHASE;
                    target = other.gameObject;
                }
            }
        }
        else if (other.tag == "Sound")
        {
            if (state != State.CHASE)
            {
                if (gameObject.tag == "Enemy")
                {
                    updatePathfinding(other.gameObject);
                    pathIndex = 1;
                    state = State.INSPECT;
                }
                else if (gameObject.tag == "Civilian")
                {
                    //TODO Add civilian reaction
                }
            }
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
        else if (state == State.INSPECT)
            speed = inspectSpeed;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));
        gameObject.GetComponent<Rigidbody>().velocity = direction.normalized * speed;

        //If the AI is too fast, we reduce his speed here.
        if (gameObject.GetComponent<Rigidbody>().velocity.sqrMagnitude > Mathf.Pow(speed, 2))
        {
            var coef = (Mathf.Abs(gameObject.GetComponent<Rigidbody>().velocity.x) + Mathf.Abs(gameObject.GetComponent<Rigidbody>().velocity.y)) / speed;
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(gameObject.GetComponent<Rigidbody>().velocity.x / coef, gameObject.GetComponent<Rigidbody>().velocity.y / coef, 0);
        }
    }

    /// <summary>
    /// Funtion to update the pathfinding according to a given goal.
    /// </summary>
    /// <param name="goal">Goal to the pathfinding.</param>
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

        foreach (var node in search.path)
        {
            var vector = new Vector3(TilesContainer[Int32.Parse(node.label)].transform.position.x, TilesContainer[Int32.Parse(node.label)].transform.position.y, 0);
            tempV3[count++] = vector;
        }

        path = new Vector3[tempV3.Length];
        path = tempV3;

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
                    if (!TilesContainer[hit.gameObject.GetComponent<Tile_Pathfinder>().getID()].GetComponent<Tile_Pathfinder>().isWall)
                    closestTile = hit;
            }
        }
        return closestTile.gameObject.GetComponent<Tile_Pathfinder>().getID();
    }

}
