using UnityEngine;
using System.Collections;

//Script for actions with buttons for the Option menu.

public class Option_menu_handler : MonoBehaviour {

    public GameObject Menu;

    public void showMenu()
    {
        gameObject.SetActive(true);
        Menu.SetActive(false);
    }

    public void hideMenu()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Function called when you press the Save & Quit button. It will apply the options selected and quit.
    /// </summary>
    public void SaveQuit()
    {

        //TODO Add saving in the opt file.
        if (FindObjectOfType<Resolution_dropdown>() != null)
            FindObjectOfType<Resolution_dropdown>().setResolution();

        hideMenu();
        Menu.SetActive(true);
        
    }

    /// <summary>
    /// Function called when you press the quit button in the option menu. It won't change the resolution.
    /// </summary>
    public void Quit()
    {
        hideMenu();
        Menu.SetActive(true);
    }

}
