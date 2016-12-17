using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class WinScene : MonoBehaviour {

public Text Score;
	// Use this for initialization
	void Start () {
	Score.text = ScoreManager.score.ToString();

	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown(KeyCode.Space)){
		SceneManager.LoadScene("Start");
		
		}

	}
}
