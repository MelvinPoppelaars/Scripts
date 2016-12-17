using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossBar : MonoBehaviour {
public static Slider Bossbar;
public static float LerpSpeed = 5f;
public static float BossMinValue = 0f;
public static float NewBossValue = 20000f;

	// Use this for initialization
	void Start () {	
	Bossbar = GetComponent<Slider>();
	Bossbar.value = BossMinValue;
	
	}


	void Update ()
	{
		Bossbar.value = Mathf.Lerp (Bossbar.value, NewBossValue, Time.deltaTime * LerpSpeed);

		if (WaveManager.WaveCount == WaveManager.BossWave) {
		}

	}

}