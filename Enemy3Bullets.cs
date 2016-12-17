using UnityEngine;
using System.Collections;

public class Enemy3Bullets : MonoBehaviour {

public GameObject Bullets;
public float BulletSpeed;
private float BulletperSecond = 0.4F;


	// Use this for initialization

	void Fire ()
	{
		GameObject Bullet = Instantiate (Bullets, transform.position, transform.rotation) as GameObject; 
		Bullet.GetComponent<Rigidbody2D>().velocity = new Vector3 (0,BulletSpeed,0) + transform.up * BulletSpeed;
	}

	void Update ()
	{
		if (Time.time >= 2f) {
		
			float Probability = Time.deltaTime * BulletperSecond;

			if (Random.value < Probability) {
				Fire ();
			}	
	}
	
	}
	
	// Update is called once per frame
}