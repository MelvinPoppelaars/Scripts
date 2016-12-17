using UnityEngine;
using System.Collections;


public class Alien1 : MonoBehaviour {
public float Health = 300;
public float Speed = 4f;
public float Step = 4f;
private GameObject Player1;
private GameObject Player2;
public  GameObject DeathSplash;
private Vector3 Player1Pos;
public GameObject Portal;
public int ScoreValue = 150;
public AudioClip Splatter;
public AudioClip BulletImpact;


	public void OnDrawGizmos ()
	{
		Gizmos.DrawWireSphere(transform.position,1f);
	}

	// Use this for initialization
	void Start ()
	{
		
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

		StartCoroutine ("ColliderSwitch");		
		GameObject Clone = Instantiate (Portal, transform.position, Quaternion.identity) as GameObject;
		Destroy (Clone, 1.5f);
		
	}


	void OnTriggerEnter2D (Collider2D Col)
	{
		// projectile is the script that is accessed and wil look for to collider2D on a gameObject that has the projectile script attached to it
		projectile missile = Col.gameObject.GetComponent<projectile> ();

		if (missile) {
			AudioSource.PlayClipAtPoint(BulletImpact, Camera.main.transform.position, 1.0F);
			Health -= missile.GetDamage ();
			StartCoroutine(colorswap());
		}
	
		if (Health <= 0) {
		// Play clip at point on Camera location to avoid effect in worldspace
			AudioSource.PlayClipAtPoint(BulletImpact, Camera.main.transform.position, 1.0F);
			AudioSource.PlayClipAtPoint(Splatter, Camera.main.transform.position, 1.0F);
			GameObject ParticleClone = Instantiate (DeathSplash, this.transform.position, Quaternion.identity) as GameObject ;
			Destroy(this.gameObject);
			Destroy(ParticleClone,2);
			ScoreManager.score += ScoreValue;

		}
}
	
	// Update is called once per frame
	void Update ()
	{	

		FollowPlayer();

	}

	IEnumerator colorswap(){

		GetComponent<SpriteRenderer>().color = Color.red;
		yield return new WaitForSeconds (0.1f);
		GetComponent<SpriteRenderer>().color = Color.white;

	}

	IEnumerator ColliderSwitch ()
	{
		GetComponent<Collider2D>().enabled = false;
		yield return new WaitForSeconds (1F);
		GetComponent<Collider2D>().enabled = true;
	}
	
	void FollowPlayer()
	{

		
		if (Player1 == null || Player2 == null) {
			Destroy (this.gameObject);
			GameObject ParticleClone = Instantiate (DeathSplash, this.transform.position, Quaternion.identity) as GameObject ;
			Destroy(ParticleClone,2);
			AudioSource.PlayClipAtPoint(Splatter, transform.position);
		}
		//Player1Pos = newVector3 looks for the x,y,z pos of Player1		
		//Player1Pos = newVector3 looks for the x,y,z pos of Player1
		Player1Pos = new Vector3 (Player1.transform.position.x, Player1.transform.position.y, transform.position.z);
		var P1Rotation = Quaternion.LookRotation (transform.position - Player1Pos, Vector3.forward);
		transform.position = Vector3.MoveTowards(transform.position,Player1Pos, Speed * Time.deltaTime);
		//if you want to manipulate an object only in one direction, you can set it's other axes to 0;
		P1Rotation.x = 0;
		P1Rotation.y = 0;
		// rotate the object only on z, Slerp gives a smooth transition between the enemy rotation towards the desired rotation
		transform.rotation = Quaternion.Slerp(transform.rotation, P1Rotation, 1.8f * Time.deltaTime);
			;
		}
		
	}





