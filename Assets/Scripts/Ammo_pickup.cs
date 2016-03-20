using UnityEngine;
using System.Collections;

public class Ammo_pickup : MonoBehaviour {

    public int WeaponID;
    public int clipDrop = 2; //Number of clip you will get by walking on it.

    public bool randomAmmo = false; //False when box drop, true when dropped by enemy
    public int minDrop = 10;
    public int maxDrop = 20;

    public AudioClip pickupSFX;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") //For the moment, only the player will restore his health... 
                                   //Maybe if the game is in an harder difficulty, they could use it
        {
            int ammo = 0;
            if (randomAmmo)
                ammo = Random.Range(minDrop, maxDrop);
            else
                ammo = clipDrop * GameObject.FindObjectOfType<Weapon_Container>().getClipSize();

            GameObject.FindObjectOfType<Player_sounds>().playSound(pickupSFX);

            GameObject.FindObjectOfType<Weapon_Container>().addAmmo(ammo);  
            GameObject.Destroy(gameObject);

        }
    }

}
