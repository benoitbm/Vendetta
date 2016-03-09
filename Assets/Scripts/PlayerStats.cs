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

	// Use this for initialization
	void Start () {
        playerHP = playerMaxHP;
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey("r"))
            playerHP--;

        if (playerHP < 0)
            playerHP = 0;

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

}
