using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour {

    PlayerStats playerstats;

    public string[] catchphrase;
    public int catchphraseNumber = Random.Range(1, 10);
    public Text catchphraseText;

    
    // Use this for initialization
    void Start () {
        playerstats = GetComponent<PlayerStats>();
        catchphrase = new string[10];

        catchphrase[0] = "One Tram driver who wants revenge!";
        catchphrase[1] = "This time it's personal.";
        catchphrase[2] = "Your path to madness is littered with your victims.";
        catchphrase[3] = "Good or Evil - it's your choice.";
        catchphrase[4] = "To want revenge is part of human nature.";
        catchphrase[5] = "Revenge is a dish best served with a bullet to the back of the head";
        catchphrase[6] = "Their fate is in your hands";
        catchphrase[7] = "Mario is relying on you.";
        catchphrase[8] = "This could be your death sentence.";
        catchphrase[9] = "To forgive is only human, but you are a monster.";
        catchphrase[10] = "The 1968 Toronto vigilante!";

        setCatchText();


    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void setCatchText() {

        catchphraseText.text = catchphrase[catchphraseNumber];

    }

    void newGame() {

        //ATM this load debugscreen.unity, but this should be changed to be level one
        Application.LoadLevel(1);

    }

    void loadGame() {

        //TODO load game variables from file.
    }

    void optionsMenu() {

    }

    void exitGame() {
        Application.Quit();
    }
}
