using UnityEngine;
using System.Collections;

public class EnemieSpawner1 : MonoBehaviour {
public GameObject[] Enemies;
private int Enemie;
public float width = 10f;
public float height = 10f;
public static bool MovingRight = false;
float SpawnDelay = 0.5f;
public int WaveLenght;
private int ChildCount = 0;
public float NextWaveDelay;
public Transform[] SpawnPoints;
private int Index;
private bool IsCoroutineStarted = false;



// Use this for initialization

	void Start ()
	{
			
			if (WaveManager.WaveCount >= 5) {
			WaveLenght++;
			}

			if (WaveManager.WaveCount >= 8) {
			WaveLenght++;
			}

			if (WaveManager.WaveCount >= 15) {
			WaveLenght = WaveLenght ++;
			}

			if (WaveManager.WaveCount >= 19) {
			WaveLenght = WaveLenght ++;
			}

			if (WaveManager.WaveCount >= 22) {
			WaveLenght = WaveLenght + 2;
			}
// Shuffle the Spawnpoints
	SpawnPoints.DoShuffle();	
	SpawnUntilFull ();
	
	}

	


	public void OnDrawGizmos () {
		Gizmos.DrawWireCube (transform.position, new Vector3 (width,height,0f));
	}
	
	// Update is called once per frame
	void Update ()
	{
				
		if (ChildCount == WaveLenght) {

			CancelInvoke ("SpawnUntilFull");
			//CancelInvoke("DoShuffle");
		
		}
		
		if (AllMembersAreDead ()) {
			IsCoroutineStarted = true;
			if (IsCoroutineStarted == true) {
			StartCoroutine (WaveReadyDelay());
			}
		}
	}

	Transform NextFreePosition ()
	{	

		// loop through all the spanwpoints to see if it is free ( in the new shuffled order)
		foreach (Transform ChildPositionGameObject in SpawnPoints) {
			if (ChildPositionGameObject.childCount == 0) {
				ChildCount++;
				return (ChildPositionGameObject);
			}
			
			}

		// anything else return nothing.
		return null;
	}


// create a bool to check if spawner is empty (either yes or no)
	bool AllMembersAreDead () {
// for every position in transform, count 
// if count is higher than 0 return value false
		foreach (Transform ChildPositionGameObject in SpawnPoints) {
			if (ChildPositionGameObject.childCount > 0) {
				return false;
			}
		}	
// if false is not returned return true;
			return true;
			
	}



	void SpawnUntilFull ()
	{	//Look for freeposition as been called by NextFreePosition
		Transform freePosition = NextFreePosition ();
		Enemie = Random.Range (0, Enemies.Length);

			if (freePosition) {	
			GameObject Enemy = Instantiate (Enemies[Enemie], freePosition.position, Quaternion.identity) as GameObject;
			//GameObject Enemy = Instantiate (Enemies, SpawnPoints[CurrentPoint].position, Quaternion.identity) as GameObject;
			Enemy.transform.parent = freePosition;
			Invoke ("SpawnUntilFull", SpawnDelay);
		}
	}
	IEnumerator WaveReadyDelay() {
		yield return new WaitForSeconds (NextWaveDelay);
		WaveManager.WaveReady = true;
		Destroy (gameObject);
		CancelInvoke ("WaveReadyDelay");
	}

}
