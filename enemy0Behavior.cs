using UnityEngine;
using System.Collections;

public class enemy0Behavior : MonoBehaviour {
public GameObject Bullet;
public GameObject DeathParticle;
public AudioClip DeathSound;
public AudioClip BulletImpact;
public AudioClip Shot;
public float BulletSpeed;
public float Probability = 0.7F;
private float EnemyShot;
public int Score;
public float Health;
	// Use this for initialization
	void Start () {
	StartCoroutine (ColliderSwitch());
	InvokeRepeating ("Fire", 1,1);
	}


	void OnTriggerEnter2D (Collider2D col)
	{
		projectile missile = col.gameObject.GetComponent<projectile> ();
		if (missile) {
			StartCoroutine (ColorSwap());
			Health -= missile.GetDamage();
			AudioSource.PlayClipAtPoint(BulletImpact, Camera.main.transform.position);
		
		}

		if (Health <= 0) {
			GameObject clone = Instantiate (DeathParticle, this.transform.position, Quaternion.identity) as GameObject;
			AudioSource.PlayClipAtPoint(DeathSound, Camera.main.transform.position);
			Destroy (clone, 2f);
			ScoreManager.score += Score;
			Destroy(this.gameObject);
			
			
			

		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void Fire() {
		EnemyShot = Random.value;

		if (EnemyShot >= Probability) {
			Vector3 Offset = new Vector3 (0,-0.5F,0);
			AudioSource.PlayClipAtPoint (Shot, Camera.main.transform.position);
			GameObject EnemyBeam = Instantiate (Bullet, this.transform.position - Offset, Quaternion.identity) as GameObject;
			EnemyBeam.GetComponent<Rigidbody2D>().velocity = new Vector3 (0, BulletSpeed);
		}

	}

	IEnumerator ColorSwap()
	{
		GetComponent<SpriteRenderer>().color = Color.red;
		yield return new WaitForSeconds (0.5f);
		GetComponent<SpriteRenderer>().color = Color.white;

	}

	IEnumerator ColliderSwitch() {
		GetComponent<PolygonCollider2D>().enabled = false;
		yield return new WaitForSeconds (1F);
		GetComponent<PolygonCollider2D>().enabled = true;
	}
}
