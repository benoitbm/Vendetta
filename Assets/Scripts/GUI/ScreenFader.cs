using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour {

    bool show = false;
    bool hide = true;

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
	}
	
    void Update()
    {
        if (show)
            showScreen();
        else if (hide)
            hideScreen();
    }

    public void showScreen()
    {
        var temp = gameObject.GetComponent<Image>().color;

        temp.a += Time.deltaTime / 2;
        gameObject.GetComponent<Image>().color = temp;

        show = (temp.a <= 1);

    }

    public void hideScreen()
    {
        var temp = gameObject.GetComponent<Image>().color;

        temp.a -= Time.deltaTime / 2;
        gameObject.GetComponent<Image>().color = temp;

        hide = (temp.a >= 0);
        if (!hide)
            gameObject.SetActive(false);
        
    }

}
