using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour {


public GameObject[] Waves = new GameObject [3];
private int ArrayLenght;
public static bool WaveReady = true;
public GameObject NextPhaseFX;
private Vector3 Particle;
public AudioClip NextPhaseClip;
public static int WaveCount;
public static int BossWave = 23;
private static int BossSpawned;
public float DelayTime;	
private float Next;
	

	void Start() {
		ArrayLenght = Waves.Length -1;
		WaveCount = 0;
		WaveReady = true;
		BossSpawned = 0;
	}

	

// put var spawndelay on NextSpawn so each time a new phase is reached the spawn time can be delayed
	void FirstSpawn() {
			Instantiate (Waves [0], transform.position, transform.rotation);
			WaveReady = false;		

	}

	void NextSpawn ()
	{		
			Instantiate (Waves [Random.Range (0, ArrayLenght)], transform.position, transform.rotation);
			WaveReady = false;
	}

	void BossSpawn ()
	{
			Instantiate (Waves[8], transform.position, transform.rotation);
			WaveCount +=1;
			BossSpawned +=1;
			WaveReady = false;
	}
	

	

	void Update ()
	{
		if (WaveReady == true && WaveCount == 0 && Time.timeSinceLevelLoad > DelayTime) {
		WaveCount += 1;
		FirstSpawn();
		
		}

		if (WaveReady == true && Time.time > DelayTime && WaveCount <= BossWave && WaveCount >= 1) {
			WaveCount += 1;
			NextSpawn ();

			if (WaveCount == 3) {
				ArrayLenght = Waves.Length - 1;
			}					
		}
		
		if (WaveReady == true && Time.time > DelayTime && WaveCount >= BossWave && BossSpawned ==0) {
		BossSpawn();
		}

	}
}
