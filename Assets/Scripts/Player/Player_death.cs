using UnityEngine;
using System.Collections;

//This script is used to have the good rotation of the body when it spawns.

public class Player_death : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (GameObject.FindObjectOfType<Moving>() != null)
            gameObject.transform.rotation = GameObject.FindObjectOfType<Moving>().transform.rotation;
	}
	
}
