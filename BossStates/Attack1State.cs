using UnityEngine;
using System.Collections;

public class Attack1State : MonoBehaviour {
	public Transform[] Positions;
	public GameObject[] Enemies;
	public float SpawnDelay = 2f;
	private int Enemy;
	private float Timer;
	public float SpawnTimer = 2f;

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
					
		if (StatePatterns.Attack1 != false) {
		InvokeRepeating ("SpawnEnemy", 1, SpawnDelay);

		}
	}
// Instantiate random enemy on random position
// 
	void SpawnEnemy() {
		Enemy = Random.Range (0, Enemies.Length);
		Vector3 position = Positions[Random.Range (0, Positions.Length)].transform.position;
		Instantiate (Enemies[Enemy], position, Quaternion.identity); 
		CancelInvoke();
	}
}
