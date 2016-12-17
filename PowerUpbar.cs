using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerUpbar : MonoBehaviour {
public static Slider PowerUpBar;
public static float LerpSpeed = 5f;
public static float MaxValue = 5;
public static float MinValue = 0;
public static float NewValue;

	// Use this for initialization
	void Start () {	
	PowerUpBar = GetComponent<Slider>();
	PowerUpBar.value = MinValue;
	
	}


	void Update() {
	
	PowerUpBar.value = Mathf.Lerp (PowerUpBar.value, NewValue, Time.deltaTime * LerpSpeed);
	}

}