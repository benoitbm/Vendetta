using UnityEngine;
using System.Collections;
using System.IO; //Used to read and write files

//Script that will handle all the option data (and go through every screen)
//It is Serializable to permit saving data

[System.Serializable]
public class data_Handler : MonoBehaviour {

    int screenW; //Screen resolution
    int screenH;
    int refreshRate = 60;
    bool fullscreen = true;
    int ID = -1; //ID for the dropdown menu (Negative if not associate)

    void Awake()
    {
        DontDestroyOnLoad(gameObject); //To keep this element in every screen
    }

	// Use this for initialization
	void Start () {
        if (System.IO.File.Exists(Application.persistentDataPath+"/options.opt"))
        {
            print("Option file exists in "+Application.persistentDataPath);
        }
        else
        {
            print("File doesn't exist, creating...");
            createOptionFile();
        }

	}
	

    void createOptionFile()
    {
        System.IO.File.CreateText(Application.persistentDataPath + "/options.opt");

        screenW = Screen.width;
        screenH = Screen.height;
        
        print("File created");
    }

    public void changeResolution(int w, int h)
    {
        screenW = w;
        screenH = h;
        Screen.SetResolution(w, h, fullscreen, refreshRate);
    }

    public void setFullscreen(bool full)
    {
        fullscreen = full;
    }

    public bool isFullscreen()
    { return fullscreen; }

    public void setID(int _ID)
    { ID = _ID; }

    public int getID()
    { return ID; }
}


