using UnityEngine;
using System.Collections;

//Script for the health and hitboxes for the items

public class Asset_hitbox : MonoBehaviour {

    public bool activateLoot = false;
    public float dropRate = 50;
    public int maxHealth = 10;
    private int currentHealth; 

	// Use this for initialization
	void Start () {
        currentHealth = maxHealth;
	}
	
    public void takeHit(int dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {

            if (activateLoot)
            {

            }

            Destroy(gameObject);
        }

    }
}
