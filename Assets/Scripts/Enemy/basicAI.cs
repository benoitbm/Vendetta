using System;
using UnityEngine;
using System.Collections;


public class basicAI : MonoBehaviour {

    public NavMeshAgent agent;
    
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
    public float chaseSpeed = 6.0f;
    public GameObject target;    

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();

        agent.updatePosition = true;
        agent.updateRotation = false;

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

    void Patrol()
    {
        print("Patrolling...");

        agent.speed = patrolSpeed;
        if (Vector3.Distance(gameObject.transform.position, waypoints[waypointIndex].transform.position) >= 2) //If he is too far from a waypoint
        {
            agent.SetDestination(waypoints[waypointIndex].transform.position);
            Move(agent.desiredVelocity);
        }
        else if (Vector3.Distance(gameObject.transform.position, waypoints[waypointIndex].transform.position) < 2) //If he is too close from a waypoint
            waypointIndex = ++waypointIndex % waypoints.Length; //He will change to the next waypoint.
        else //Else, he won't move.
            Move(Vector3.zero);
    }

    void Chase()
    {
        agent.speed = chaseSpeed;
        agent.SetDestination(target.transform.position);
        Move(agent.desiredVelocity);
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
    /// This script will make the AI moving. Based on ThirdPersonCharacter standard asset and adapted for our game.
    /// </summary>
    /// <param name="move">Vector to the position wanted.</param>
    public void Move(Vector3 move)
    {
        if (move.magnitude > 1f)
            move.Normalize(); //We normalize for better manipulations
        move = transform.InverseTransformDirection(move);
        move = Vector3.ProjectOnPlane(move, Vector3.forward);
        transform.Rotate(0, 0, Mathf.Atan2(move.x, move.y) * Time.deltaTime);
        gameObject.GetComponent<Rigidbody>().velocity = move;
    }

}
