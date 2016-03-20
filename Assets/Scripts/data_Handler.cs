﻿using UnityEngine;
using System.Collections;
using System.IO; //Used to read and write files

//Script that will handle all the data (and go through every screen)

public class data_Handler : MonoBehaviour {

    int screenX; //Screen resolution
    int screenY;
    
    bool fullscreen;

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
	
	// Update is called once per frame
	void Update () {
	
	}

    void createOptionFile()
    {
        System.IO.File.CreateText(Application.persistentDataPath + "/options.opt");
        print("File created");
    }
}
