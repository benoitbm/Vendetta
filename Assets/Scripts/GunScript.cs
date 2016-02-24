using UnityEngine;
using System.Collections;
using System.IO;

//Script for firing and weapon, each weapon will have his own settings with Unity configuration
public class GunScript : MonoBehaviour {

    public float fireRate = 3; //Bullet per second (TOCHECK : See if it really work (can be improved)
    public bool autofire = false; //Enables autofire if the weapon allow it (Mostly uzi and thompson)

    public GameObject bullet;
    public GameObject player; //For rotation
    public GameObject spawnBullet; //Mostly, a small cube without mesh and collision at the end of the gun.

    public AudioClip fireFX; //Sound of shot

    private float fireDelay = 0; //Variable for the delay between last shot and current press.

    void Start()
    {
        this.gameObject.AddComponent<AudioSource>(); //Adding the elements for the sound
        this.GetComponent<AudioSource>().clip = fireFX;
    }

    void Update()
    {
        if ((Input.GetButton("Fire1") && autofire && Time.time > fireDelay) || (Input.GetButtonDown("Fire1") && Time.time > fireDelay))
        {
            fireDelay = Time.time + fireRate/60; //TOCHECK if we can replace the 60 by the last FPS known.

            Instantiate(bullet, spawnBullet.transform.position, player.transform.localRotation); //Creation of the bullet with position at the end of the gun (rotation is done in bullet_move.cs)
            this.GetComponent<AudioSource>().Play(); //Playing the sound everytime we shoot

        }

    }

	void FixedUpdate() 
    {
	    
	}
}
