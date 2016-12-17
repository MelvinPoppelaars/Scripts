using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class lifeUI : MonoBehaviour {
private GameObject Life1;
private GameObject Life2;



	// Use this for initialization
	void Start () {

		Life1 = GameObject.Find("Life1");
		Life2 = GameObject.Find("Life2");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	if (Player1Controller.PlayerLives <= 2) {
		Life1.GetComponent<Image>().enabled = false;

		} else {
			Life1.GetComponent<Image>().enabled = true;

		}
		
		if (Player1Controller.PlayerLives <= 1) {
			Life2.GetComponent<Image>().enabled = false;
		} else {
			Life2.GetComponent<Image>().enabled = true;
		}

	}

}