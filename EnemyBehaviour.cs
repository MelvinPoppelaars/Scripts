using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
public float health = 150;
public int scoreValue = 100;
public float BulletSpeed = 0f;
public GameObject EnemyBullet;
public GameObject Explosion;
public float BulletPerSecond = 0.5f;
public AudioClip EnemieLaser;
public AudioClip EnemieDeath;
public AudioClip BulletImpact;
public Sprite []EnemySprites;
//public GameObject EnemyHit;



void Start ()
	{
	StartCoroutine(ColliderSwitch());
	}	

void OnTriggerEnter2D (Collider2D Col)
	{	// return value if a collission with projectile is detected
		projectile missile = Col.gameObject.GetComponent<projectile> ();
	
		
		if (missile) {
			AudioSource.PlayClipAtPoint(BulletImpact,Camera.main.transform.position, 1.0f);
			
			health -= missile.GetDamage (); 
			StartCoroutine (ChangeColor ());
		}
		

		if (health <= 0) {
			AudioSource.PlayClipAtPoint(BulletImpact,Camera.main.transform.position, 1.0f);
			AudioSource.PlayClipAtPoint (EnemieDeath, Camera.main.transform.position, 1.0f);
		// store the explosion in a new GameObject so we can destroy that game object after 1f
			GameObject Clone = (GameObject) Instantiate(Explosion, transform.position, Quaternion.identity);
			Destroy (Clone, 2f);
			Destroy (gameObject);
			ScoreManager.score += scoreValue;
		}
	}
	void Update ()
	{	

	// create a new float where the interval is time * 0.5
		float probability = Time.deltaTime * BulletPerSecond;
		// if the random value (which is between 0 and 1) is lower than 0.5 
		if (Random.value < probability) {
			Fire ();
		}	

		if (EnemySpawner.MovingRight == true)
			GetComponent<SpriteRenderer>().sprite = EnemySprites[2]; 


		if (EnemySpawner.MovingRight == false)
			GetComponent<SpriteRenderer>().sprite = EnemySprites[1]; 

		
	}


	// create a variable for instantiating a bullet
	void Fire ()
	{
		GameObject enemybeam = Instantiate (EnemyBullet, this.transform.position, Quaternion.identity) as GameObject;
		enemybeam.GetComponent <Rigidbody2D>().velocity = new Vector3 (0,BulletSpeed);	
		AudioSource.PlayClipAtPoint(EnemieLaser, Camera.main.transform.position);
	}
	
	//IEnumerator change spritecolor and wait for time
	IEnumerator ChangeColor ()
	{
		GetComponent<SpriteRenderer> ().color =	(Color.red);
		//Yield return new overwrites 
		yield return new WaitForSeconds (0.1f);
		GetComponent<SpriteRenderer> ().color =	(Color.white);
	}

	IEnumerator ColliderSwitch ()
	{
		GetComponent<Collider2D>().enabled = false;
		yield return new WaitForSeconds (1F);
		GetComponent<Collider2D>().enabled = true;
	}

}
