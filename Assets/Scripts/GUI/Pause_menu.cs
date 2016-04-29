using UnityEngine;
using System.Collections;

//Script helping to communicate with the pause menu

public class Pause_menu : MonoBehaviour {

    public GameObject pauseMenu;
    public GameObject GUI;

    bool onPause = false;
    bool transitionScreen = false;
    bool pauseDisabled = false;

	// Use this for initialization
	void Start () {
        pauseMenu.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("escape") && !pauseDisabled && !transitionScreen)
        {
            onPause = !onPause;

            if (onPause)
                pauseMenu.gameObject.SetActive(true);
        }

        PauseHandler();
	}

    public void resumeGame()
    {
        onPause = false;
    }

    void PauseHandler()
    {
        if (pauseDisabled || transitionScreen)
        {
            Time.timeScale = 1.0f;
            GUI.gameObject.SetActive(false);
        }
        else if (onPause)
        {
            Time.timeScale = 0.0f; //Stopping time
            GUI.gameObject.SetActive(false);

            if (FindObjectOfType<AudioSource>())
            {
                AudioSource[] ALs = FindObjectsOfType<AudioSource>();
                foreach (AudioSource al in ALs)
                    al.Pause();
            }
        }
        else
        {
            Time.timeScale = 1.0f;
            pauseMenu.gameObject.SetActive(false);
            GUI.gameObject.SetActive(true);

            if (FindObjectOfType<Option_menu_handler>())
                FindObjectOfType<Option_menu_handler>().gameObject.SetActive(false);

            if (FindObjectOfType<AudioSource>())
            {
                AudioSource[] ALs = FindObjectsOfType<AudioSource>();
                foreach (AudioSource al in ALs)
                    al.UnPause();
            }
        }
    }

    /// <summary>
    /// This function will be used to know if the game is paused or not.
    /// </summary>
    /// <returns>Returns a boolean which indicates if the game is paused.</returns>
    public bool pausedGame()
    { return onPause; }

    public void activateTransion()
    { transitionScreen = true; }

    public void disablePause()
    { pauseDisabled = true; }

}
