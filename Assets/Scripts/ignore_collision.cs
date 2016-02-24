using UnityEngine;
using System.Collections;

//This script is for ignoring collision of the arm with "low items" (as tables)

public class ignore_collision : MonoBehaviour {

    void OnCollisionEnter(Collision collision)
    {

        //if (collision.gameObject.tag == "Bullet_through")
        {
            //Physics.IgnoreCollision(collision.GetComponent<Collider>(), GetComponent<Collider>());
        }

    }

}
