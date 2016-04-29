using UnityEngine;
using System.Collections;

//This script is used for the camera. It follows the player.
public class FollowPlayer : MonoBehaviour {

    GameObject player;
    bool follow = true;

    void Start()
    {
        if (GameObject.FindObjectOfType<Moving>() != null)
            player = GameObject.FindObjectOfType<Moving>().gameObject;
    }


    // Update is called once per frame
	void LateUpdate() 
    {
        if (player != null && follow)
        {
            Vector3 playerPos = player.transform.position;
            transform.position = new Vector3(playerPos.x, playerPos.y, playerPos.z - 10);
        }
        
	}

    public void stopFollow()
    { follow = false;}

    public void resumeFollow()
    { follow = true; }

    public void changeTarget(GameObject target)
    { player = target; }
}
