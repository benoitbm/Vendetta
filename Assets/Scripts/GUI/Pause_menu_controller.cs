using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//The script controller used with the Pause menu Canvas

public class Pause_menu_controller : MonoBehaviour {

    public GameObject player;
    public GameObject optionMenu;
    public GameObject screenFader;

    public void resume()
    {
        player.GetComponent<Pause_menu>().resumeGame();
    }

    public void gotoMenu()
    {
        player.GetComponent<Pause_menu>().activateTransion();
        screenFader.SetActive(true);
        screenFader.GetComponent<ScreenFader>().showScreen();
        StartCoroutine(transitionToMainMenu());
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void displayOptionMenu()
    {
        optionMenu.GetComponent<Option_menu_handler>().showMenu();
    }

    IEnumerator transitionToMainMenu()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("MainMenu");
    }
}
