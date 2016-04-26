using UnityEngine;
using System.Collections;

public class Enemy_controller : MonoBehaviour {

    public int enemyStartHP = 15;
    private int enemyHP;

    private int dmgCoef = 1; //Damage coefficient based on difficulty.
    private int HPCoef = 1; //Health coefficient based on difficulty.

    public GameObject EnemyDead;

	// Use this for initialization
	void Start () {
        //TODO Add difficulty modifiers
        enemyStartHP *= HPCoef;
        enemyHP = enemyStartHP;
	}

    /// <summary>
    /// This function is used to add or remove health from the enemy health.
    /// </summary>
    /// <param name="HP">Int modifier. Can be positive to restore HP, or negative to deal damage.</param>
    public void updateHP(int HP)
    {
        enemyHP += HP;
        if (enemyHP > enemyStartHP)
            enemyHP = enemyStartHP;
        else if (enemyHP <= 0)
        {
            enemyHP = 0;
            enemyDeath();
        }
    }

    /// <summary>
    /// This function is used to remove health. If not sure about how to remove health, use this one instead of updateHP.
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

    void enemyDeath()
    {
        var child = gameObject.transform.GetChild(gameObject.transform.childCount - 1);
        var newchild = Instantiate(EnemyDead);
        newchild.transform.rotation = child.transform.rotation;
        newchild.transform.position = child.transform.position;

        child.transform.parent = null;
        Destroy(child.gameObject);
        newchild.transform.parent = gameObject.transform;
    }
}
