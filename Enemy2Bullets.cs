using UnityEngine;
using System.Collections;

public class Enemy2Bullets : MonoBehaviour {
public GameObject Bullet;
public float BulletSpeed;
public float BulletDelay;

	// Use this for initialization

	void OnDrawGizmos ()
	{
		Gizmos.DrawWireSphere (transform.position, 0.1f);
	}

	void Start () {
	// put fire invoke repeating to create a delay on bullet shooting on start because invoke repeating updates itself, doens't work in update!
		InvokeRepeating("Fire", 1.6F, BulletDelay);
	}

	void Fire ()
	{
	//instantiate the EnemyBullet object and store it in a new game object.
		GameObject EnemyBullet = Instantiate(Bullet, transform.position ,transform.rotation) as GameObject;
	// acces the ridgebody and assign a movement on the velocity: x + the rotation of transform.up
		EnemyBullet.GetComponent <Rigidbody2D>().velocity = new Vector3 (0,BulletSpeed) + transform.up * BulletSpeed;	

	}


}
