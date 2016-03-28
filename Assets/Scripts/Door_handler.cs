using UnityEngine;
using System.Collections;

public class Door_handler : MonoBehaviour {

    public Rigidbody door;

    bool playerIn = false;


    void Update()
    {
        var angle = door.transform.localRotation.z;

        if (Mathf.Abs(angle) > .75 && playerIn)
            door.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        else if (Mathf.Abs(angle) > .75)
            door.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
            playerIn = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerIn = false;
         
    }
	
}

