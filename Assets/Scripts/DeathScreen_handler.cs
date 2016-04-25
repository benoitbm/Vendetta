using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//This script is used for the death screen. It will control and handle all the functions based on the death screen.

public class DeathScreen_handler : MonoBehaviour {

    public GameObject transitionScreen;
    public Image Background;
    public Text deathText;
    public Text commentText;
    public GameObject bRestart;
    public GameObject bMenu;

    bool choiceMade = false;

	// Use this for initialization
	void Start () {
        choseDeathPhrase();
        Background.color = new Color(0f, 0f, 0f, 0f);
        deathText.gameObject.SetActive(false);
        commentText.gameObject.SetActive(false);
        bRestart.SetActive(false);
        bMenu.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

        var tempColor = Background.color;

        if (tempColor.a < 100.0f / 255.0f)
        {
            tempColor.a += Time.deltaTime / 3;
            Background.color = tempColor;
        }
        else if (choiceMade) //Case when a choice has been made.
        {
            deathText.gameObject.SetActive(false);
            commentText.gameObject.SetActive(false);
            bRestart.SetActive(false);
            bMenu.SetActive(false);
        }
        else
        {
            deathText.gameObject.SetActive(true);
            commentText.gameObject.SetActive(true);
            bRestart.SetActive(true);
            bMenu.SetActive(true);
        }
	}

    /// <summary>
    /// Function for the button Restart Level on the death screen.
    /// </summary>
    public void bRestartLevel()
    {
        choiceMade = true;
        transitionScreen.SetActive(true);
        transitionScreen.GetComponent<ScreenFader>().showScreen();
        StartCoroutine(restartLevel());
    }
    
    /// <summary>
    /// Funtion for the button Return to main menu on the death screen.
    /// </summary>
    public void bReturnToMenu()
    {
        choiceMade = true;
        transitionScreen.SetActive(true);
        transitionScreen.GetComponent<ScreenFader>().showScreen();
        StartCoroutine(gotoMenu());
    }

    /// <summary>
    /// Function used to determine which death sentance the game has to display.
    /// </summary>
    void choseDeathPhrase()
    {
        var deathSentance = new string[7];

        deathSentance[1] = "We warned you about the Mafia.";
        deathSentance[2] = "A man who desires vengeance dig two graves. Except only yours will be used.";
        deathSentance[3] = "They were not joking when they said that they will get you.";
        deathSentance[4] = "Your body was used as an example.";
        deathSentance[5] = "You will join your mother in few moments.";
        deathSentance[6] = "Mario will join you later, don't worry.";

        var chosenSentace = Random.Range(1, deathSentance.Length);
        commentText.text = deathSentance[chosenSentace];
    }

    IEnumerator restartLevel()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator gotoMenu()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("MainMenu");
    }
}
