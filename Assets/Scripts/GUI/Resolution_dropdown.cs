using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic; //For list

//Script for the resolution dropdown list in the options menu 

public class Resolution_dropdown : MonoBehaviour {

    Resolution[] res;
    Dropdown dd;

    GameObject dataHandler;

    int resX, resY;

	// Use this for initialization
	void Start () {
        if (GameObject.FindObjectOfType<data_Handler>() != null)
            dataHandler = GameObject.FindObjectOfType<data_Handler>().gameObject;

        dd = gameObject.GetComponent<Dropdown>();

        res = Screen.resolutions;

        List<string> list = new List<string>();

        for (int i = 1; i < res.Length; ++i)
            list.Add(getResolution(i));

        dd.AddOptions(list);
        dd.onValueChanged.AddListener(delegate { getValues(res[dd.value+1].width, res[dd.value+1].height); });
        dd.value = res.Length - 1;
    }

    string getResolution(int i)
    { return res[i].width + "x" + res[i].height; }

    public void setResolution()
    {dataHandler.GetComponent<data_Handler>().changeResolution(resX, resY); }

    public void getValues(int w, int h)
    {
        resX = w;
        resY = h;
    }
}
