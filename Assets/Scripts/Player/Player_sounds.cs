using UnityEngine;
using System.Collections;

public class Player_sounds : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.gameObject.AddComponent<AudioSource>();
    }
	
    /// <summary>
    /// Funtion that will play a sound once.
    /// </summary>
    /// <param name="sfx">Audioclip to play once.</param>
    public void playSound(AudioClip sfx)
    {
        GetComponent<AudioSource>().PlayOneShot(sfx);
    }

    /// <summary>
    /// Funtion that will play a sound once.
    /// </summary>
    /// <param name="sfx">Audioclip to play once.</param>
    /// <param name="percent">Modify the output volume.</param>
    public void playSound(AudioClip sfx, float percent)
    {
        GetComponent<AudioSource>().PlayOneShot(sfx, percent);
    }
}
