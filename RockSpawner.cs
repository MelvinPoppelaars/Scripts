using UnityEngine;
using System.Collections;

public class RockSpawner : MonoBehaviour {
public float speed;
public GameObject[] Enemies;
float spawner = 0.015f;
	// Use this for initialization
	void Start () {
	

	}
	public void OnDrawGizmos() {
	Gizmos.DrawWireSphere (transform.position,1);

	}


	// Update is called once per frame
	void Update ()
	{
	
		// makes a float that has a value of time * spawner
		float probability = Time.deltaTime * spawner;
		// if the random value (which is between 0 and 1) is lower than 0.5 
		if (Random.value < probability) {
			// instantiating a random object from the array
				Instantiate (Enemies [Random.Range (0, Enemies.Length)], transform.position, Quaternion.identity);		
			}
		}
		
	}

