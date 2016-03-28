using UnityEngine;
using System.Collections;
using UnityEngine.UI; //For HP Bar

//Script witch wiil be the "inventory" of the player.
//Check UML file for more informations.
public class PlayerStats : MonoBehaviour {

    private int playerHP;
    public int playerMaxHP = 100;

    public Image hpBar; //The front health bar, not the background
    public Text HPText;

    public GameObject Trajan_Dead;
    public GameObject Trajan_onehand;

	// Use this for initialization
	void Start () {
        playerHP = playerMaxHP;
	
	}
	
	// Update is called once per frame
	void Update () {

        //Used to test, remove before final submit. DEBUG
        if (Input.GetKey("o") && playerHP>0)
            playerHP--;

        if (playerHP == 0 && Input.GetKeyDown("o"))
            playerHP--;

        if (playerHP < 0)
        {
            playerHP = 0;
            playerDeath();
        }

        updateHPBar();
	}
    
    /// <summary>
    /// This function will be used to get current HP.
    /// </summary>
    /// <returns>It will return the current health (int)</returns>
    int getHP()
    { return playerHP; }

    /// <summary>
    /// This function is used to update the HP Bar. 
    /// </summary>
    void updateHPBar()
    {
        hpBar.fillAmount = ((float)playerHP / playerMaxHP);

        if (hpBar.fillAmount > .5)
            hpBar.color = new Color(0, .9f, 0);
        else if (hpBar.fillAmount <= .5 && hpBar.fillAmount > .25)
            hpBar.color = Color.yellow;
        else
            hpBar.color = new Color(.9f, 0, 0);

        HPText.text = playerHP.ToString();
    }

    /// <summary>
    /// This function is used to add or remove health from the player health.
    /// </summary>
    /// <param name="HP">Int modifier. Can be positive to restore HP, or negative to deal damage.</param>
    public void updateHP(int HP)
    {
        playerHP += HP;
        if (playerHP > playerMaxHP)
            playerHP = playerMaxHP;
        else if (playerHP <= 0)
        {
            playerHP = 0;
            playerDeath();
        }
    }

    void playerDeath()
    {
        var child = gameObject.transform.GetChild(gameObject.transform.childCount - 1);
        var newchild = Instantiate(Trajan_Dead);
        newchild.transform.rotation = child.transform.rotation;
        newchild.transform.position = child.transform.position;

        child.transform.parent = null;
        Destroy(child.gameObject);
        newchild.transform.parent = gameObject.transform;
    }

}
