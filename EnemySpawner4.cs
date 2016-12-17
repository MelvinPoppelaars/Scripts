using UnityEngine;
using System.Collections;

public class EnemySpawner4 : MonoBehaviour {
public GameObject Enemy4;
public static bool MovingRight = false;
private float xMin;
private float xMax;
float SpawnDelay = 0.5f;
public float NextWaveDelay;
public Transform[] SpawnPoints;
public int Wavelenght;
private int ChildCount = 0;
private bool IsCoroutineStarted = false;

	// Use this for initialization
	void Start () {
	
			
			if (WaveManager.WaveCount >= 10) {
			Wavelenght++;
			}

			if (WaveManager.WaveCount >= 15) {
			Wavelenght++;
			}

			if (WaveManager.WaveCount >= 20) {
			Wavelenght++;
			}

			
	SpawnPoints.DoShuffle();
		WaveManager.WaveReady = false;
		SpawnUntilFull();
		
	
		}


	public void OnDrawGizmos () {
		Gizmos.DrawWireCube (transform.position, new Vector3 (1f,1f,0f));
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (Wavelenght == ChildCount) {
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
	{	// if the ChildPositionGameObject counts 1 object (==0) return the ChildPositionGameObject 
		foreach (Transform ChildPositionGameObject in SpawnPoints) {
			if (ChildPositionGameObject.childCount == 0) {
				ChildCount++;
				return ChildPositionGameObject;
			}
		}
		// otherwise return nothing.
		return null;

}


// create a bool to check if spawner is empty (either yes or no)
	bool AllMembersAreDead () {
// for every position in transform, count 
// if count is higher than 0 return value false
		foreach (Transform childPositionGameObject in SpawnPoints) {
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
		if (freePosition) {	
			GameObject Enemy = Instantiate (Enemy4, freePosition.position, Quaternion.identity) as GameObject;
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
