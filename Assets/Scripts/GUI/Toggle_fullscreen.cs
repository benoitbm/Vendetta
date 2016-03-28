using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Script for the toggle fullscreen in the options menu

public class Toggle_fullscreen : MonoBehaviour {

    Toggle go;

	// Use this for initialization
	void Start () {
        if (GameObject.FindObjectOfType<data_Handler>() != null) //Just some safeguards, we never knows.
        {
            go = gameObject.GetComponent<Toggle>();
            go.isOn = GameObject.FindObjectOfType<data_Handler>().isFullscreen();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (GameObject.FindObjectOfType<data_Handler>() != null)
        {
            if (go.isOn)
                GameObject.FindObjectOfType<data_Handler>().setFullscreen(true);
            else
                GameObject.FindObjectOfType<data_Handler>().setFullscreen(false);
        }
    }
}
