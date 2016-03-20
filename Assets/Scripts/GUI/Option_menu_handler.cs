using UnityEngine;
using System.Collections;

public class Option_menu_handler : MonoBehaviour {

    public GameObject mainMenu;

    public void showMenu()
    {
        gameObject.SetActive(true);
    }

    public void hideMenu()
    {
        gameObject.SetActive(false);
    }

    public void SaveQuit()
    {
        //TODO Add saving in the opt file.

        hideMenu();
        mainMenu.GetComponent<MenuController>().showMenu();
    }

    public void Quit()
    {
        hideMenu();
        mainMenu.GetComponent<MenuController>().showMenu();
    }
}
