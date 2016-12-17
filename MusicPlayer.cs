using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

static int count = 0;

public AudioClip StartClip;
public AudioClip GameClip;
public AudioClip EndClip;

private AudioSource Music;


	void Start ()
	{

		Music = GetComponent<AudioSource>();
		Music.loop = true;
		Music.clip = StartClip;
		Music.Play();
	// makes sure there will only be one audiosource, if the second one appears on load it will be destroyed
		if (count == 0) {
		
			GameObject.DontDestroyOnLoad (gameObject);
			count ++;
		} else if(count >= 1) {
		
			Destroy(gameObject);
		}	
}

	

	void Update() {

	}


	// level is the scene in the buildhyrarchy, it checks the level and plays the suggested clip
	void OnLevelWasLoaded (int level)
	{
		Debug.Log (level + "level");
		
		if (level == 0) {
			Music.clip = StartClip;
			Music.Play ();
		}

		if (level == 1) {
			Music.clip = GameClip;
			Music.Play ();
		}

		if (level == 2) {
			Music.clip = EndClip;
			Music.Play ();
		}

	}
}
	
