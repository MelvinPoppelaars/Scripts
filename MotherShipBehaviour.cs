using UnityEngine;
using System.Collections;

public class MotherShipBehaviour : MonoBehaviour {
public Transform[] SpawnPoints;
public GameObject[] turret;
public GameObject CloneShip;
private int CurrentTurret;
private float width = 1;
private float height = 1;
private int ChildCount = 0;
public int WaveLenght;
public float NextWaveDelay;
private bool IsCoroutineStarted = false;

private GameObject Clone;




	// Use this for initialization
	void Start ()
	{		

		if (WaveManager.WaveCount >= 5) {
			WaveLenght ++;
			}


		if (WaveManager.WaveCount >= 8) {
			WaveLenght ++;
			}

		if (WaveManager.WaveCount >= 14) {
			WaveLenght++;
			}

		if (WaveManager.WaveCount >= 20) {
			WaveLenght++;
			}

		if (WaveManager.WaveCount >= 22) {
		WaveLenght = WaveLenght +2;
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
			Instantiate (CloneShip, this.transform.position, Quaternion.identity);
			Destroy (gameObject);
			

//			rend.enabled = false;
	//		StartCoroutine (WaveReady());
			}
		}
	}

	Transform NextFreePosition ()
	{	
		// if the ChildPositionGameObject counts 0 objects (==0) return the ChildPositionGameObject 
		foreach (Transform ChildPositionGameObject in SpawnPoints) {
			if (ChildPositionGameObject.childCount == 0) {
				ChildCount++;
				return (ChildPositionGameObject);
				}
			}

		return null;
		// anything else return nothing.
	
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
		//instantiate the Enemy GameObject at the freeposition method
		CurrentTurret = Random.Range (0, turret.Length);



			if (freePosition) {	

			GameObject Enemy = Instantiate (turret[CurrentTurret], freePosition.position, Quaternion.identity) as GameObject;
			Enemy.transform.parent = freePosition;
			Invoke ("SpawnUntilFull", 0.05F);
			
		}
	}


	IEnumerator WaveReady() {
	yield return new WaitForSeconds (NextWaveDelay);
	WaveManager.WaveReady = true;
	Destroy (gameObject);	
	CancelInvoke ("WaveReady");

	}
}

