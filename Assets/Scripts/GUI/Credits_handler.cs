using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//This script is for the credits screen.

public class Credits_handler : MonoBehaviour {

    public GameObject transitionScreen;

	public void bSurvey()
    {
        Application.OpenURL("https://docs.google.com/forms/d/154FQvE_W9tkNqG6VW8hATuW4ELdk2WHln3FD_pRQLos/viewform");
    }

    public void bMenu()
    {
        transitionScreen.SetActive(true);
        transitionScreen.GetComponent<ScreenFader>().showScreen();
        StartCoroutine(transitionToMenu());
    }

    IEnumerator transitionToMenu()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("MainMenu");
    }
}
