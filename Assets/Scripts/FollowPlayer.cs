using UnityEngine;
using System.Collections;

//This script is used for the camera. It follows the player.
public class FollowPlayer : MonoBehaviour {

    GameObject player;

    void Start()
    {
        if (GameObject.FindObjectOfType<Moving>() != null)
            player = GameObject.FindObjectOfType<Moving>().gameObject;
    }


    // Update is called once per frame
	void LateUpdate() 
    {
        if (player != null)
        {
            Vector3 playerPos = player.transform.position;
            transform.position = new Vector3(playerPos.x, playerPos.y, playerPos.z - 10);
        }
        
	}
}
