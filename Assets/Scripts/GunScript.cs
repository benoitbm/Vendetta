using UnityEngine;
using System.Collections;
using System.IO;

//Script for firing and weapon, each weapon will have his own settings with Unity configuration
public class GunScript : MonoBehaviour {

    public float fireRate = 3; //Bullet per second (TOCHECK : See if it really work (can be improved)
    public bool autofire = false; //Enables autofire if the weapon allow it (Mostly uzi and thompson)
    public ushort bulletDamage = 3;

    public GameObject bullet;
    public GameObject player; //For rotation
    public GameObject spawnBullet; //Mostly, a small cube without mesh and collision at the end of the gun.

    public AudioClip fireFX; //Sound of shot
    public AudioClip emptyFX; //Dry fire sound
    public AudioClip reloadFX; //Reload sound

    private float fireDelay = 0; //Variable for the delay between last shot and current press.

    private Weapon_Container parent_weapon = null;

    void Start()
    {
        this.gameObject.AddComponent<AudioSource>(); //Adding the elements for the sound

        parent_weapon = gameObject.transform.root.GetComponent<Weapon_Container>();
    }

    void Update()
    {
        if (gameObject.transform.parent.tag == "Player") //Checking if it's the player...
        {
            if (!GameObject.Find("Player_controller").GetComponent<Pause_menu>().pausedGame())
            {
                if ((Input.GetButton("Fire1") && autofire && Time.time > fireDelay) || (Input.GetButtonDown("Fire1") && Time.time > fireDelay))
                {
                    if (gameObject.GetComponent<Weapon>().shot())
                    {
                        fireDelay = Time.time + fireRate / 60; //TOCHECK if we can replace the 60 by the last FPS known.

                        GameObject tempbullet = Instantiate(bullet, spawnBullet.transform.position, player.transform.localRotation) as GameObject; //Creation of the bullet with position at the end of the gun (rotation is done in bullet_move.cs)
                        tempbullet.transform.GetComponentInChildren<Bullet_Hitbox>().setDMG(bulletDamage);
                        this.GetComponent<AudioSource>().PlayOneShot(fireFX, .25f); //Playing the sound everytime we shoot
                    }
                    else if (parent_weapon.autoReload && !gameObject.GetComponent<Weapon>().WisReloading() && gameObject.GetComponent<Weapon>().canReload())
                    {
                        this.GetComponent<AudioSource>().PlayOneShot(reloadFX);
                        gameObject.GetComponent<Weapon>().reload();
                    }
                    else if (!gameObject.GetComponent<Weapon>().WisReloading())
                        this.GetComponent<AudioSource>().PlayOneShot(emptyFX);

                }

                if ((Input.GetKeyDown("r")) && gameObject.GetComponent<Weapon>().canReload() && !gameObject.GetComponent<Weapon>().WisReloading())
                {
                    this.GetComponent<AudioSource>().PlayOneShot(reloadFX);
                    gameObject.GetComponent<Weapon>().reload();
                }
            }
        }
        else //Enemy side
        {
            
            if (Input.GetKeyDown("p"))
            {
                Instantiate(bullet, spawnBullet.transform.position, player.transform.localRotation); //Creation of the bullet with position at the end of the gun (rotation is done in bullet_move.cs)
                this.GetComponent<AudioSource>().PlayOneShot(fireFX, .25f); //Playing the sound everytime we shoot
            }
        }

    }

	void FixedUpdate() 
    {
	    
	}
}
