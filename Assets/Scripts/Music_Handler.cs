using UnityEngine;
using System.Collections;

public class Music_Handler : MonoBehaviour {

    public AudioClip musicIntro;
    public AudioClip musicLoop;
    public float volume = 1f;

	// Use this for initialization
	void Start () {
        gameObject.AddComponent<AudioSource>();
        StartCoroutine(playMusic());
    }
	
	IEnumerator playMusic()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(musicIntro, volume);
        yield return new WaitForSeconds(musicIntro.length);
        gameObject.GetComponent<AudioSource>().Stop();
        gameObject.GetComponent<AudioSource>().loop = true;
        gameObject.GetComponent<AudioSource>().clip = musicLoop;
        gameObject.GetComponent<AudioSource>().Play();
    }
}
