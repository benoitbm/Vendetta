using UnityEngine;
using System.Collections;

//This script will detect the collision between the bullet and other items, and will remove HP if needed.

public class Bullet_Hitbox : MonoBehaviour {

    public GameObject bulletself;
    private int dmg;

    //TODO
    //According to the bullet, add a function which removes health to other items (if not a wall)

    void Start()
    {
        GameObject tempWeapon = GameObject.FindGameObjectWithTag("Weapon");
        dmg = tempWeapon.GetComponent<GunScript>().bulletDamage;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != null)
        {
            if (other.tag != "Bullet_through")
            {
                Rigidbody.DestroyObject(bulletself);
                if (other.gameObject.GetComponent<Asset_hitbox>())
                    other.gameObject.GetComponent<Asset_hitbox>().takeHit(dmg);

            }
        }
    }
}
