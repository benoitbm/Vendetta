using UnityEngine;
using System.Collections;

//Script for the health and hitboxes for the items

public class Asset_hitbox : MonoBehaviour {

    public bool activateLoot = false;
    public float dropRate = 50; //Percentage to know the chance to drop an element.
    public float dropMedikit = 50; //If it drops something, the rate to have a medikit. (Else, it will be ammo)

    public GameObject medikit_asset;
    public GameObject ammo_asset;

    public int maxHealth = 10;
    private int currentHealth; 

	// Use this for initialization
	void Start () {
        currentHealth = maxHealth;
        PlayerPrefs.Save();
	}
	
    public void takeHit(int dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {

            if (activateLoot)
            {
                if (Random.Range(0, 100) < dropRate)
                {
                    if (Random.Range(0, 100) < dropMedikit)
                        Instantiate(medikit_asset, gameObject.transform.position, gameObject.transform.rotation);
                    else
                        Instantiate(ammo_asset, gameObject.transform.position, gameObject.transform.rotation);

                }

            }

            Destroy(gameObject);
        }

    }
}
