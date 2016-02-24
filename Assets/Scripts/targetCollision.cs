using UnityEngine;
using System.Collections;

//Small script to test the destruction of elements. Used in debug room.
public class targetCollision : MonoBehaviour {

    public GameObject self;

    void OnTriggerEnter(Collider other)
    {
        print("Tag = " + other.tag);
        if (other.tag == "Bullet")
            Destroy(self);

    }
}

