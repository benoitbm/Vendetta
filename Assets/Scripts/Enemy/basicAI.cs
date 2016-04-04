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
    private int waypointIndex = 0;
    public float patrolSpeed = 4.0f;

    //Chase variables
    public float chaseSpeed = 5.5f;
    public GameObject target;    

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
        if (Vector3.Distance(gameObject.transform.position, waypoints[waypointIndex].transform.position) >= 2) //If he is too far from a waypoint
        {
            Move(waypoints[waypointIndex].transform.position);
        }
        else if (Vector3.Distance(gameObject.transform.position, waypoints[waypointIndex].transform.position) < 2) //If he is too close from a waypoint
            waypointIndex = ++waypointIndex % waypoints.Length; //He will change to the next waypoint.
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

}
