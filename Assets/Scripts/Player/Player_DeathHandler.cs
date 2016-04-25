using UnityEngine;
using System.Collections;

//Script to activate the Death Screen. To be put on the player controller.

public class Player_DeathHandler : MonoBehaviour {

    public GameObject GUI;
    public GameObject deathScreen;

    public void playerDeath()
    {
        gameObject.GetComponent<Pause_menu>().disablePause();
        deathScreen.SetActive(true);
    }

}
