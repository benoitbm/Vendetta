using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//The script controller used with the Pause menu Canvas

public class Pause_menu_controller : MonoBehaviour {

    public GameObject player;
    public GameObject optionMenu;

    public void resume()
    {
        player.GetComponent<Pause_menu>().resumeGame();
    }

    public void gotoMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void displayOptionMenu()
    {
        gameObject.SetActive(false);
        optionMenu.SetActive(true);
    }
}
