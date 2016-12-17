using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour {
public static int score;
private int startwave;
public Text Score;
public Text Wave;


	// Use this for initialization
	void Awake () {
		Score = GetComponent<Text>();
		score = 0;

	}

	
	// Update is called once per frame
	void Update ()
	{
		Score.text = ("" + score);
	
		if (WaveManager.WaveCount == 0) {
			Wave.text = ("Wave 1");
		} else if (WaveManager.WaveCount >= 0) {
			Wave.text = ("Wave " +WaveManager.WaveCount);
		}


	}

	// if the game scene loads the score should be reset.

}
