using UnityEngine;
using System.Collections;

public class SmallEnemy1 : MonoBehaviour {


public int BulletSpeed;
public float health;
public AudioClip BulletImpact;
public AudioClip DeathExplosion;
public AudioClip BulletSound;
public GameObject Bullet;
public GameObject[] Item;
private float probability = 0.2F;
private float ItemPickUp;
private int SpawnItem;

public GameObject DeathParticle;
public int ScoreValue = 100;
private float BulletDelay = 1f;



	// Use this for initialization



	void Start() {
	InvokeRepeating("Fire", 1, BulletDelay);
	ItemPickUp = Random.value;
	SpawnItem = Random.Range (0, Item.Length);

	}

	void OnTriggerEnter2D (Collider2D col)
	{
		// look for collision with gameObject projectile
		projectile missile = col.gameObject.GetComponent<projectile> ();

		if (missile) {
			AudioSource.PlayClipAtPoint (BulletImpact, Camera.main.transform.position);
			health -= missile.GetDamage ();
			StartCoroutine (ChangeColor ());
		}

		if (health <= 0) {
			GameObject Clone = Instantiate (DeathParticle, transform.position, Quaternion.identity) as GameObject;
			Destroy (Clone, 2f);
			AudioSource.PlayClipAtPoint (BulletImpact, Camera.main.transform.position);
			AudioSource.PlayClipAtPoint (DeathExplosion, Camera.main.transform.position);
			ScoreManager.score += ScoreValue; 
			Destroy (this.gameObject);

			if (ItemPickUp >= probability) {
				Instantiate (Item[SpawnItem], transform.position, Quaternion.identity);
			}
		}
	
	}

	// Update is called once per frame
	void Update ()
	{
	Destroy(gameObject,7.2f);
	
	}


	void Fire () {

		GameObject Beam = Instantiate (Bullet, this.transform.position, this.transform.rotation) as GameObject;
		Beam.GetComponent<Rigidbody2D>().velocity =  new Vector3 (0, BulletSpeed);
		AudioSource.PlayClipAtPoint (BulletSound, Camera.main.transform.position);
		

	}

	IEnumerator ChangeColor(){
		GetComponent<SpriteRenderer>().color = Color.red;
		yield return new WaitForSeconds (0.1f);
		GetComponent<SpriteRenderer>().color = Color.white; 
	}

	IEnumerator FireBullets() {
		Fire();
		yield return new WaitForSeconds (0.2f);
		Fire();
		yield return new WaitForSeconds (0.2f);
		Fire();


	}

}
