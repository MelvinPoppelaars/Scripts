using UnityEngine;
using System.Collections;

public class Attack2State : MonoBehaviour {
	public Transform[] Positions;
	public GameObject Enemy;
	public float SpawnDelay = 0.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (StatePatterns.Attack2 == true) {
		InvokeRepeating ("SpawnEnemies", SpawnDelay, 0F);
		}
	//	Enemie = Random.Range (0, Enemies.Length);
	}

	void SpawnEnemies() {
		Vector3 Position = Positions[Random.Range(0,Positions.Length)].transform.position;
		Instantiate (Enemy, Position, Quaternion.identity);
		CancelInvoke();
	
	}
}
