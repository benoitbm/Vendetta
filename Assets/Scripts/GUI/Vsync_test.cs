using UnityEngine;
using System.Collections;
//This script is for test Vsync (and smoothing a bit the game, just trying)
public class Vsync_test : MonoBehaviour {

    public bool activated1 = false;
    private bool activated2 = false; //Double VSync seems to reduce game performances and makes some bugs. To avoid (for the moment)

	// Use this for initialization
	void Start () {
	    if (activated2)
        {
            QualitySettings.vSyncCount = 2; //Number of Vertical Synchronisations between each frames. (here 2)

        }
        else if (activated1)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
	}

}
