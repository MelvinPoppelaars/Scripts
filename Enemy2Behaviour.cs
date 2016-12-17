using UnityEngine;
using System.Collections;

public class Enemy2Behaviour : MonoBehaviour {
public float health = 200f;
public int scoreValue = 300;
public GameObject Explosion;
public AudioClip DeathSound;
public AudioClip BulletImpact;
//public GameObject EnemyHit;

	// Use this for initialization
	void Start () {
	StartCoroutine ("ColliderSwitch");
	}
	
	void OnTriggerEnter2D (Collider2D Col)
	{
		//search for the projectile gameObject to get collision with so it can deal its damage
		projectile missile = Col.gameObject.GetComponent<projectile> ();
	
		if (missile) {
			AudioSource.PlayClipAtPoint(BulletImpact, Camera.main.transform.position);
			health -= missile.GetDamage (); 
			StartCoroutine (ChangeColor());
			//Instantiate (EnemyHit, this.transform.position, Quaternion.identity);
		}
		
		if (health <= 0) {
			AudioSource.PlayClipAtPoint(BulletImpact, Camera.main.transform.position);
			AudioSource.PlayClipAtPoint(DeathSound, Camera.main.transform.position);
			GameObject Clone = (GameObject) Instantiate (Explosion, transform.position, Quaternion.identity);
			Destroy (Clone, 2f);
			Destroy (gameObject);
			ScoreManager.score += scoreValue;

		}
		
	}
	
	IEnumerator ChangeColor(){
		GetComponent<SpriteRenderer>().color = Color.red;
		yield return new WaitForSeconds (0.1f);
		GetComponent<SpriteRenderer>().color = Color.white; 
	}

	IEnumerator ColliderSwitch ()
	{
		GetComponent<Collider2D>().enabled = false;
		yield return new WaitForSeconds (1.5F);
		GetComponent<Collider2D>().enabled = true;
	}

}