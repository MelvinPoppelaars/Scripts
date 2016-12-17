using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
public GameObject Enemy1;
public float width = 10f;
public float height = 10f;
public float speed = 1f;
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
	void Start ()
	{
		if (WaveManager.WaveCount >= 3) {
			Wavelenght++;
			speed += 1;
		}


		if (WaveManager.WaveCount >= 5) {
			Wavelenght++;
		}

		if (WaveManager.WaveCount >= 9) {
			Wavelenght++;
			speed += 1;

		}

		if (WaveManager.WaveCount >= 15) {
			Wavelenght = Wavelenght + 2;
		}

		if (WaveManager.WaveCount >= 20) {
			Wavelenght = Wavelenght + 2;
			speed += 1;

		}

		if (WaveManager.WaveCount >= 22) {
			Wavelenght = Wavelenght + 4;
		}



		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		//create a new vector3 edge of left and right side 
		Vector3 LeftEdge = Camera.main.ViewportToWorldPoint (new Vector3 (0,0, distanceToCamera));
		Vector3 RightEdge = Camera.main.ViewportToWorldPoint (new Vector3 (1,0, distanceToCamera));
		//assign the new edge on the x position in the float xMin and xMax
		xMin = LeftEdge.x;
		xMax = RightEdge.x;
		
		SpawnPoints.DoShuffle();
		WaveManager.WaveReady = false;
		SpawnUntilFull();
		
	
		}


	public void OnDrawGizmos () {
		Gizmos.DrawWireCube (transform.position, new Vector3 (width,height,0f));
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (Wavelenght == ChildCount) {
			CancelInvoke ("SpawnUntilFull");
		}

		// the bool if movingright (either left or right) 
		if (MovingRight) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		} else {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		//shift edge of gizmo from centre to left edge and right edge
		float leftEdgeFormation = transform.position.x - 0.5f * width;
		float rightEdgeFormation = transform.position.x + 0.5f * width;
// if (leftEdgeFormation = smaller than xMin (reaches the edge OR rightEdgeFormation greater than xMax
		if (leftEdgeFormation < xMin) {
			MovingRight = true;

			// += increment (increasing in number) so ad Vector3 to transform.position in direction up times float Up
			
		} else if (rightEdgeFormation > xMax) {
			MovingRight = false; 

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
			GameObject Enemy = Instantiate (Enemy1, freePosition.position, Quaternion.identity) as GameObject;
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
