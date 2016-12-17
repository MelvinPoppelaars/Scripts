using UnityEngine;
using System.Collections;

public class MotherShipDestroyed : MonoBehaviour {
public Transform[] SpawnPoints;
private int SpawnPoint;
public GameObject[] Explosions;
private int Explosion;
public AudioClip[] ExplosionClip;
private int AudioExplosion;
public float NextWaveDelay = 3;
public float SpawnDelay;

	// Use this for initialization
	void Start () {
	SpawnUntilFull();
	StartCoroutine (WaveReady());
	
	}
	
	// Update is called once per frame
void Update ()
	{
	}

	Transform NextFreePosition ()
	{	
		// if the ChildPositionGameObject counts 0 objects (==0) return the ChildPositionGameObject 
		foreach (Transform ChildPositionGameObject in SpawnPoints) {
			if (ChildPositionGameObject.childCount == 0) {
				return (ChildPositionGameObject);
				}
			}

		return null;
		// anything else return nothing.
	
	}

	void SpawnUntilFull ()
	{	//Look for freeposition as been called by NextFreePosition
		Transform freePosition = NextFreePosition ();
		//instantiate the Enemy GameObject at the freeposition method
		Vector3 Location = SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform.position;

			if (freePosition) {	
			Explosion = Random.Range (0, Explosions.Length);
			AudioExplosion = Random.Range (0,ExplosionClip.Length);
			AudioSource.PlayClipAtPoint (ExplosionClip[AudioExplosion], Camera.main.transform.position);
			GameObject Enemy = Instantiate (Explosions[Explosion], Location, Quaternion.identity) as GameObject;
			Enemy.transform.parent = freePosition;
			Destroy (Enemy,2f);
			Invoke ("SpawnUntilFull", SpawnDelay);
			
			
		}
	}
	
	IEnumerator WaveReady() {
	yield return new WaitForSeconds (NextWaveDelay);
	WaveManager.WaveReady = true;
	Destroy (gameObject);	
	CancelInvoke ("WaveReady");
	}
}