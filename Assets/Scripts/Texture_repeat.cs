using UnityEngine;
using System.Collections;

//This script is used when you want to have a mosaic texture on any surface.

public class Texture_repeat : MonoBehaviour {
    /// <summary>
    /// XRepeat is the number you want to repeat on x-axis.
    /// </summary>
    public float XRepeat = 64;
    /// <summary>
    /// YRepeat is the number you want to repeat on y-axis.
    /// </summary>
    public float YRepeat = 64;

	// Use this for initialization
	void Start () {
        transform.GetComponent<Renderer>().material.mainTextureScale = new Vector2(XRepeat, YRepeat);
    }

}
