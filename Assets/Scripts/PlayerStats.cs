using UnityEngine;
using System.Collections;

//Script witch wiil be the "inventory" of the player.
//Check UML file for more informations.
public class PlayerStats : MonoBehaviour {

    private int playerHP;
    public int playerMaxHP = 100;

	// Use this for initialization
	void Start () {
        playerHP = playerMaxHP;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
