using UnityEngine;
using System.Collections;

public class Alien1Spawner : MonoBehaviour {
public GameObject Enemies;
public float width = 10f;
public float height = 10f;
public float speed = 1f;
float SpawnDelay = 0.5f;
public int WaveLenght;
private int ChildCount = 0;
public Transform[] SpawnPoints;
public float WaveDelay = 2f;
private int Index;
private bool IsCoroutineStarted = false;
public static bool NextWaveDelay = false;

	// Use this for initialization
	void Start ()
	{
		if (WaveManager.WaveCount >= 3) {
			WaveLenght ++;
			}


		if (WaveManager.WaveCount >= 6) {
			WaveLenght ++;
			}

		if (WaveManager.WaveCount >= 8) {
			WaveLenght ++;
			}

		if (WaveManager.WaveCount >= 10) {
			WaveLenght = WaveLenght +2;
			}

		if (WaveManager.WaveCount >= 15) {
		WaveLenght = WaveLenght +2;
		}

		if (WaveManager.WaveCount >= 20) {
		WaveLenght = WaveLenght +4;
		}
		
		SpawnPoints.DoShuffle();
		SpawnUntilFull ();
		WaveManager.WaveReady = false;
		}


	public void OnDrawGizmos () {
		Gizmos.DrawWireCube (transform.position, new Vector3 (width,height,0f));
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (ChildCount == WaveLenght) {
			CancelInvoke ("SpawnUntilFull");
		}
		
		if (AllMembersAreDead ()) {
			IsCoroutineStarted = true;
			if (IsCoroutineStarted == true) { 
			StartCoroutine (WaveReadyDelay ());
			}
	
		}
	}

	Transform NextFreePosition ()
	{	

		// if the ChildPositionGameObject counts 1 object (==0) return the ChildPositionGameObject 
		foreach (Transform ChildPositionGameObject in transform) {
			if (ChildPositionGameObject.childCount == 0) {
				ChildCount++;
				return (ChildPositionGameObject);			
			} 
			
			if (ChildPositionGameObject.childCount > 1) {
			return null;

			}



			}
		// anything else return nothing.
		return null;
	}

	


// create a bool to check if spawner is empty (either yes or no)
	bool AllMembersAreDead ()
	{
// for every position in transform, count 
// if count is higher than 0 return value false
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount > 0) {
				return false;
			}

		}	
// if false is not returned return true;
			return true;
			
	}

	void SpawnUntilFull ()
	{	//Look for freeposition as been called by NextFreePosition
		Transform freePosition = NextFreePosition ();
		// if there is one, instantiate the Enemy GameObject at the freeposition method
		int CurrentPoint = Random.Range(0,SpawnPoints.Length);
	


			if (freePosition) {	
		// Instantiate the Enemies on the Position of the Spawnpoint
			GameObject Enemy = Instantiate (Enemies, SpawnPoints[CurrentPoint].position, Quaternion.identity) as GameObject;
		// Instantiate the Enemy to a random parent 
			Enemy.transform.parent = SpawnPoints[CurrentPoint];
			Invoke ("SpawnUntilFull", SpawnDelay);
			
		}
	}

	IEnumerator WaveReadyDelay() {
		yield return new WaitForSeconds (WaveDelay);
		WaveManager.WaveReady = true;
		Destroy (gameObject);
		CancelInvoke ("WaveReadyDelay");
	}

}

