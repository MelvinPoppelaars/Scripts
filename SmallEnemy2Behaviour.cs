using UnityEngine;
using System.Collections;

public class SmallEnemy2Behaviour : MonoBehaviour {
public float Health = 100f;
private Rigidbody2D rb;
public GameObject DeathParticles;
public AudioClip HitClip;
public GameObject[] Item;
public int Scorevalue;
private float probability = 0.5F;
private float ItemSpawn;
private int SpawnItem;
public AudioClip DeathSplash;
	// Use this for initialization
	void Start () {
	
	ItemSpawn = Random.value;
	SpawnItem = Random.Range (0, Item.Length);
	rb = GetComponent<Rigidbody2D>();
	}

	void Update ()
	{
		rb.velocity = new Vector2 (0,2);
	}
	
	void OnTriggerEnter2D (Collider2D Col)
	{
		projectile missile = Col.gameObject.GetComponent<projectile> ();
		
		if (missile) {
			Health -= missile.GetDamage (); 
			StartCoroutine (ColorSwap ());
			AudioSource.PlayClipAtPoint (HitClip, Camera.main.transform.position);

			if (Health <= 0) {
				ScoreManager.score += Scorevalue;
				GameObject Clone = Instantiate (DeathParticles, this.transform.position, transform.rotation) as GameObject;
				Destroy (Clone, 2F);
				AudioSource.PlayClipAtPoint (DeathSplash, Camera.main.transform.position);
				Destroy (this.gameObject);
				
				if (ItemSpawn >= probability) {
					Instantiate (Item[SpawnItem], transform.position, Quaternion.identity);
				}
				
			}
		
		}
	}
	


	IEnumerator ColorSwap(){

		GetComponent<SpriteRenderer>().color = Color.red;
		yield return new WaitForSeconds (0.5f);
		GetComponent<SpriteRenderer>().color = Color.white;
	}

}
