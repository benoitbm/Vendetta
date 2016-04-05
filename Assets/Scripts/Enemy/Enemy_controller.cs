using UnityEngine;
using System.Collections;

public class Enemy_controller : MonoBehaviour {

    public int enemyStartHP = 15;
    private int enemyHP;

    private int dmgCoef = 1; //Damage coefficient based on difficulty.
    private int HPCoef = 1; //Health coefficient based on difficulty.

	// Use this for initialization
	void Start () {
        //TODO Add difficulty modifiers
        enemyStartHP *= HPCoef;
        enemyHP = enemyStartHP;
	}
	
	// Update is called once per frame
	void Update () {
    
	}

    /// <summary>
    /// This function is used to add or remove health from the enemy health.
    /// </summary>
    /// <param name="HP">Int modifier. Can be positive to restore HP, or negative to deal damage.</param>
    public void updateHP(int HP)
    {
        enemyHP += HP;
        print("Enemy got " + enemyHP + " on " + enemyStartHP + "HP");
        if (enemyHP > enemyStartHP)
            enemyHP = enemyStartHP;
        else if (enemyHP <= 0)
        {
            enemyHP = 0;
            Destroy(gameObject); //TODO : Add true death.
        }
    }

    /// <summary>
    /// This function is used to remove health. It will automatically remove health. If not sure about how to remove health, use this one instead of updateHP.
    /// <seealso cref="updateHP(int)"/>
    /// </summary>
    /// <param name="dmg">Health to remove.</param>
    public void takeDMG(int dmg)
    {
        updateHP(-Mathf.Abs(dmg));
    }

    public void shoot()
    {
        gameObject.GetComponentInChildren<GunScript>().enemyShot(dmgCoef);
    }
}
