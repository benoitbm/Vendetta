using UnityEngine;
using System.Collections;

public class Player_death : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (GameObject.FindObjectOfType<Moving>() != null)
            gameObject.transform.rotation = GameObject.FindObjectOfType<Moving>().transform.rotation;
	}
	
}
