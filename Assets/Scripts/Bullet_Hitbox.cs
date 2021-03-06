﻿using UnityEngine;
using System.Collections;

//This script will detect the collision between the bullet and other items, and will remove HP if needed.

public class Bullet_Hitbox : MonoBehaviour {

    public GameObject bulletself;
    int dmg = 5;

    //TODO
    //According to the bullet, add a function which removes health to other items (if not a wall)

    public void setDMG(int _dmg)
    { dmg = _dmg; }

    public void setDMG(float _dmg)
    { dmg = Mathf.RoundToInt(_dmg); }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != null)
        {   
            if (other.tag != "Bullet_through" && other.tag != "Sound")
            {
                //Rigidbody.DestroyObject(bulletself);
                Destroy(gameObject.transform.parent.gameObject);
                if (other.gameObject.GetComponent<Asset_hitbox>() != null) //If asset which can be destroyed
                    other.gameObject.GetComponent<Asset_hitbox>().takeHit(dmg);

                else if (other.gameObject.transform.GetComponentInParent<PlayerStats>()) //If player
                    other.gameObject.transform.GetComponentInParent<PlayerStats>().takeDMG(dmg);

                else if (other.gameObject.transform.GetComponentInParent<Enemy_controller>() && other.tag == "Enemy") //If enemy
                    other.gameObject.transform.GetComponentInParent<Enemy_controller>().takeDMG(dmg);

            }
        }
    }
}
