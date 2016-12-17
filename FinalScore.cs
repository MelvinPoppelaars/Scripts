using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class FinalScore : MonoBehaviour {
private int WaveScore;
public Text Score;
public Text Wave;
public Slider FinalWave;

public void loadlevel (string name)
	{
			SceneManager.LoadScene (name);
	}


	// Use this for initialization
	void Start () {
		Score.text = ScoreManager.score.ToString();
		WaveScore = WaveManager.WaveCount;
		Wave.text = "Wave " + WaveScore + "/25";
	}

	// Update is called once per frame
	void Update ()
	{
		FinalWave.value = Mathf.Lerp (FinalWave.value, WaveScore, Time.deltaTime * 2f);
	
		if (Input.GetKey (KeyCode.Space)){
		loadlevel("Start");
		
		}

	}
}
