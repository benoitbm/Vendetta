using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement; //Used to load a level

//Script used for the main menu. It will contain the buttons events + random catch phrases on the main menu.

public class MenuController : MonoBehaviour {

    //PlayerStats playerstats;

    private string[] catchphrase;
    public Text catchphraseText;

    private ushort catchphraseNumber = 0;

    public GameObject OptionsMenu;

    GameObject screenfader;

    // Use this for initialization
    void Start () {
        screenfader = GameObject.FindObjectOfType<ScreenFader>().gameObject;

        catchphrase = new string[13];

        catchphrase[1] = "One Tram driver who wants revenge!";
        catchphrase[2] = "This time it's personal.";
        catchphrase[3] = "Your path to madness is littered with your victims.";
        catchphrase[4] = "Good or Evil - it's your choice.";
        catchphrase[5] = "To want revenge is part of human nature.";
        catchphrase[6] = "Revenge is a dish best served with a bullet to the back of the head";
        catchphrase[7] = "Their fate is in your hands";
        catchphrase[8] = "Mario is relying on you.";
        catchphrase[9] = "This could be your death sentence.";
        catchphrase[10] = "To forgive is only human, but you are a monster.";
        catchphrase[11] = "The 1968 Toronto vigilante!";
        catchphrase[12] = "One man. One gun. Many deaths.";

        setCatchText();
    }

    /// <summary>
    /// Function used to set a new catch phrase.
    /// </summary>
    void setCatchText() {
        catchphraseNumber = (ushort)Random.Range(1, catchphrase.Length);
        catchphraseText.text = catchphrase[catchphraseNumber];
    }

    public void newGame() {

        //ATM this load debugscreen.unity, but this should be changed to be level one
        screenfader.SetActive(true);
        screenfader.GetComponent<ScreenFader>().showScreen();
        StartCoroutine(transitionToLevel("debug_AI"));
        //SceneManager.LoadScene("level_1");

    }

    public void loadGame() {

        //TODO load game variables from file.
    }

    /// <summary>
    /// Used for the options menu in the main menu. Will display the options menu.
    /// </summary>
    public void optionsMenu() {
        hideMenu();
        OptionsMenu.GetComponent<Option_menu_handler>().showMenu();
    }

    public void creditsButton()
    {
        screenfader.SetActive(true);
        screenfader.GetComponent<ScreenFader>().showScreen();
        StartCoroutine(transitionToLevel("Credits"));
    }

    /// <summary>
    /// Used for exit button. Will quit the game.
    /// </summary>
    public void exitGame() {
        Application.Quit();
    }

    public void showMenu()
    {
        gameObject.SetActive(true);
    }

    public void hideMenu()
    {
        gameObject.SetActive(false);
    }

    IEnumerator transitionToLevel(string level)
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(level);
    }
}
