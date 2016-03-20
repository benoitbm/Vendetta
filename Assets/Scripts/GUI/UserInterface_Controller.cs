using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System; //For Math.Floor

//Script where we will show all the informations for the player.

public class UserInterface_Controller : MonoBehaviour {

	public Text timeText;

	// Use this for initialization
	void Start () {
		setTimeText ();	
	}
	
	// Update is called once per frame
	void Update () {
		setTimeText ();
        setAmmoText();
	}

    /// <summary>
    /// Function to set the text of the Timer.
    /// Takes no paramater.
    /// Returns nothing.
    /// </summary>
	void setTimeText ()
	{
        var minutes = Math.Floor(Time.timeSinceLevelLoad / 60.0f); //Math.Floor round to the lower int
        var seconds = Time.timeSinceLevelLoad % 60.0f; //Time.timeSinceLevelLoad is the time passed since the level is loaded
        timeText.text = "Time : " + minutes.ToString("00") + ":" + seconds.ToString("00.00");

	}

    void setAmmoText()
    {

    }
}
