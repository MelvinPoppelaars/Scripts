using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {
float Damage = 100f;
public float BulletSpeed;
private GameObject Player1;
private GameObject Player2;
public GameObject Explosion;
public GameObject HitParticle;
public AudioClip DeathExplosion;
private Vector3 Player1Pos;



	// Use this for initialization

	void OnDrawGizmos ()
	{
		Gizmos.DrawWireSphere (transform.position, 0.1f);
	}

	public float GetDamage() {

		return Damage;
	}

	void Start ()
	{
		
		// put fire invoke repeating to create a delay on bullet shooting on start because invoke repeating updates itself, doens't work in update!
		Player1 = GameObject.Find ("Player1");
		Player2 = GameObject.Find ("Player2");

		if (Player2 == null) {
			Player2 = Player1;
		}

		if (Player1 == null) {
			Player1 = Player2;
		}
		if (Player1 == null && Player2 == null) {
			return;
		}

	}


	void OnTriggerEnter2D (Collider2D col)
	{
// store the particle object in a new gameObject so we can destroy that gameObject after time
		GameObject Clone = Instantiate (Explosion, this.transform.position, Quaternion.identity) as GameObject;
		Destroy (Clone, 4f);
		AudioSource.PlayClipAtPoint (DeathExplosion, Camera.main.transform.position);
		Destroy(this.gameObject);
	
							
	}

	void FollowPlayer1 ()
	{

		if (Player1 == null || Player2 == null) {
			GameObject Clone = Instantiate (Explosion, this.transform.position, Quaternion.identity) as GameObject;
			Destroy (Clone, 4f);
			Destroy(this.gameObject);
		
		}

		Player1Pos = new Vector3 (Player1.transform.position.x, Player1.transform.position.y, Player1.transform.position.z);
		transform.position = Vector3.MoveTowards(transform.position,Player1Pos, BulletSpeed * Time.deltaTime);
		var P1Rotation = Quaternion.LookRotation (transform.position - Player1Pos, Vector3.forward);
		P1Rotation.x = 0;
		P1Rotation.y = 0;
		transform.rotation = Quaternion.Slerp(transform.rotation, P1Rotation, 5f * Time.deltaTime);
	
	}

	void Update ()
	{
	StartCoroutine(StartMove());

		if (transform.position == Player1Pos || Player1 == null && Player2 == null) {
			GameObject Clone = Instantiate (Explosion, this.transform.position, Quaternion.identity) as GameObject;
			Destroy (Clone, 4f);
			Destroy(this.gameObject);
		}
	}
	
			
	IEnumerator StartMove() {

		GetComponent<Rigidbody2D>().velocity = new Vector2 (0,3);
		yield return new WaitForSeconds (0.5f);
		GetComponent<Rigidbody2D>().velocity = new Vector2 (0,0);
		FollowPlayer1();
	}
}
