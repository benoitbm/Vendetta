using UnityEngine;
using System.Collections;

//Script helping to communicate with the pause menu

public class Pause_menu : MonoBehaviour {

    public GameObject pauseMenu;
    public GameObject GUI;

    private bool onPause = false;

	// Use this for initialization
	void Start () {
        pauseMenu.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	    
        if (Input.GetKeyDown("escape"))
            onPause = !onPause;

        PauseHandler();
	}

    public void resumeGame()
    {
        onPause = false;
    }

    void PauseHandler()
    {
        if (onPause)
        {
            Time.timeScale = 0.0f; //Stopping time
            pauseMenu.gameObject.SetActive(true);
            GUI.gameObject.SetActive(false);
        }
        else
        {
            Time.timeScale = 1.0f;
            pauseMenu.gameObject.SetActive(false);
            GUI.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// This function will be used to know if the game is paused or not.
    /// </summary>
    /// <returns>Returns a boolean which indicates if the game is paused.</returns>
    public bool pausedGame()
    { return onPause; }

}
