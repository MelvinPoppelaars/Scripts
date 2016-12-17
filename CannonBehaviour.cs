using UnityEngine;
using System.Collections;

public class CannonBehaviour : MonoBehaviour {
public float Health = 300;
public int ScoreValue = 300;
private Vector3 Player1Pos;
private Vector3 Player2Pos;
private GameObject Player1;
private GameObject Player2;
public GameObject Bullet;
public float BulletSpeed = 5f;
public float BulletDelay = 2f;
public float FirstShotDelay = 1f;
public AudioClip Impact;
public AudioClip DeathSound;
public GameObject DeathParticle;
private GameObject Clone;
public GameObject CannonDestroyed;
private SpriteRenderer Renderer;
private Color color;


	// Use this for initialization
	void Start () {
	Player1 = GameObject.Find ("Player1");
	Player2 = GameObject.Find ("Player2");
		Renderer =	GetComponent<SpriteRenderer>();
		color = Renderer.color;

	if (Player2 == null) {
			Player2 = Player1;
		}

	if (Player1 == null) {
			Player1 = Player2;
		}

	if (Player1 == null && Player2 == null) {
		return;
		}

	InvokeRepeating ("Fire", FirstShotDelay, BulletDelay);

	}
	


	void OnTriggerEnter2D (Collider2D col)
	{
		projectile missile = col.gameObject.GetComponent<projectile> ();

		if (missile) {	
			Health -= missile.GetDamage ();
			StartCoroutine (ColorSwap());
			AudioSource.PlayClipAtPoint (Impact, Camera.main.transform.position);
		}
	
		if (Health <= 0) {
			AudioSource.PlayClipAtPoint (DeathSound, Camera.main.transform.position);
			//Instantiate (CannonDestroyed, transform.position,Quaternion.identity);
			Clone = Instantiate (DeathParticle, transform.position, Quaternion.identity) as GameObject;
			Destroy (Clone, 4f);
			Destroy(this.gameObject);
			ScoreManager.score += ScoreValue;
		}
	

	}
	// Update is called once per frame
	void Update ()
	{

		FollowPlayer();

	}


	void Fire ()
	{
	//instantiate the EnemyBullet object and store it in a new game object.		
		GameObject EnemyBullet = Instantiate (Bullet, transform.position, transform.rotation) as GameObject;
	// intantiates the GameObject as a child, on the same transform as the parent
		//EnemyBullet.transform.parent = transform;
	// acces the ridgebody and assign a movement on the velocity: x + the rotation of transform.up
		EnemyBullet.GetComponent <Rigidbody2D>().velocity = new Vector3 (0,0) + transform.up * BulletSpeed;
	}


	void FollowPlayer()
	{
		//Player1Pos = newVector3 looks for the x,y,z pos of Player1
		Player1Pos = new Vector3 (Player1.transform.position.x, Player1.transform.position.y, transform.position.z);
		var P1Rotation = Quaternion.LookRotation (transform.position - Player1Pos, Vector3.forward);
		//if you want to manipulate an object only in one direction, you can set it's other axes to 0;
		P1Rotation.x = 0;
		P1Rotation.y = 0;
		// rotate the object only on z, Slerp gives a smooth transition between the enemy rotation towards the desired rotation
		transform.rotation = Quaternion.Slerp(transform.rotation, P1Rotation, 1.8f * Time.deltaTime);


		
	}


	IEnumerator ColorSwap() {
		GetComponent<SpriteRenderer>().color = Color.red;
		yield return new WaitForSeconds (0.1f);
		GetComponent<SpriteRenderer>().color = color;
	}


}

