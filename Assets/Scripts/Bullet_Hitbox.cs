using UnityEngine;
using System.Collections;

//This script will detect the collision between the bullet and other items, and will remove HP if needed.

public class Bullet_Hitbox : MonoBehaviour {

    public GameObject bulletself;

    //TODO
    //According to the bullet, add a function which removes health to other items (if not a wall)

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Bullet_through")
        {
            Rigidbody.DestroyObject(bulletself);
        }
    }
}
