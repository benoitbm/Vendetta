using UnityEngine;
using System.Collections;

//Script used for the medikit asset

public class Medikit_script : MonoBehaviour {

    public int HPRestorer = 25;

    public AudioClip pickupSFX;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") //For the moment, only the player will restore his health... 
                                   //Maybe if the game is in an harder difficulty, they could use it
        {
            GameObject.FindObjectOfType<Player_sounds>().playSound(pickupSFX);
            GameObject.FindObjectOfType<PlayerStats>().updateHP(HPRestorer); //Now you restore the health. 
            GameObject.Destroy(gameObject);
            
        }
    }
}
