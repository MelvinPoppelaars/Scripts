using UnityEngine;
using System.Collections;

public class Enemy3Behaviour : MonoBehaviour {
public float health;
public GameObject Rockets; 
public int ScoreValue = 500;
public int Speed = 2;
public static bool MovingRight = false;
public AudioClip Impact;
public AudioClip Explosion;
public GameObject DeathExplosion;


	// Use this for initialization
	void Start () {
	}

	void OnTriggerEnter2D (Collider2D Col)
	{
	
		projectile missile = Col.gameObject.GetComponent<projectile> ();
	
		if (missile) {
			health -=missile.GetDamage ();
			StartCoroutine (ColorSwap());
			AudioSource.PlayClipAtPoint (Impact, Camera.main.transform.position);
	
		if (health <= 0) {
			ScoreManager.score += ScoreValue;
			Destroy (this.gameObject);
			AudioSource.PlayClipAtPoint (Explosion, Camera.main.transform.position);
			GameObject Clone = Instantiate (DeathExplosion, this.transform.position, Quaternion.identity) as GameObject;
			Destroy (Clone,4f); 
			}

		}
	
	}
		
	IEnumerator ColorSwap() {
		GetComponent<SpriteRenderer>().color = Color.red;
		yield return new WaitForSeconds (0.1f);
		GetComponent<SpriteRenderer>().color = Color.white;
	}

}
