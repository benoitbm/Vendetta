using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class SplashScreen_Handler : MonoBehaviour {

    public Image blackScreen;
    public Image transitionScreen;

	// Use this for initialization
	void Start () {
        blackScreen.color = new Color(0, 0, 0, 1);
        transitionScreen.color = new Color(1, 1, 1, 0);
        StartCoroutine(transitionToMainMenu());
	}

    void Update()
    {
        if (Time.timeSinceLevelLoad < 4f)
        {
            var temp = blackScreen.color;
            temp.a -= Time.deltaTime/2;
            blackScreen.color = temp;
        }
        else
        {
            var temp = transitionScreen.color;
            temp.a += Time.deltaTime/2;
            transitionScreen.color = temp;
        }
    }
	
    IEnumerator transitionToMainMenu()
    {
        yield return new WaitForSeconds(6.5f);
        SceneManager.LoadScene("MainMenu");
    }
}
