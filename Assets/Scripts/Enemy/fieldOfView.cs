using UnityEngine;
using System.Collections;

//Script used on the cone for the vision of the NPC
public class fieldOfView : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        gameObject.transform.GetComponentInParent<basicAI>().onCollision(other);
    }

    void OnTriggerStay(Collider other)
    {
        gameObject.transform.GetComponentInParent<basicAI>().onCollision(other);
    }
}
