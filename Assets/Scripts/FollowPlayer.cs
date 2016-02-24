using UnityEngine;
using System.Collections;

//This script is used for the camera. It follows the player.
public class FollowPlayer : MonoBehaviour {

    public GameObject player;

    // Update is called once per frame
	void LateUpdate() 
    {
        Vector3 playerPos = player.transform.position;
        transform.position = new Vector3(playerPos.x, playerPos.y, -10);
        
	}
}
