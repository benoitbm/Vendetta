using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SplashScreen_Handler : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(transitionToMainMenu());
	}
	
    IEnumerator transitionToMainMenu()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("MainMenu");
    }
}
