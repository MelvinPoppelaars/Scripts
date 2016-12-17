using UnityEngine;
using System.Collections;

public class Player1Controller : MonoBehaviour {

public float speed = 1.0f;
public GameObject[] Bullet;
public float BulletDelay = 0.3f;
private float PowerUpBarMax;
public Sprite[] Ship;
public Sprite[] HeadSprite;
public AudioClip ItemPickUp;
public AudioClip PlayerHitClip;
public static int PlayerLives;
public static int PowerUpScore;
AudioSource laser;
Animator Anim;
public GameObject PlayerClone;
public GameObject PlayerHitFX;
public GameObject ItemPickUpFX;
public GameObject LifePickUpFX;
private GameObject Muzzle;
private GameObject Head;


	// Use this for initialization
	void Start ()
	{		
		 GetComponent<SpriteRenderer>().sprite = Ship[0];

//	PowerUpBarMin = PowerUpbar.PowerUpBar.value = 0F;
//	PowerUpBarMax = PowerUpbar.PowerUpBar.value = 1F;
//	PowerUpbar.PowerUpBar.value = PowerUpbarmin;
		Anim = GetComponent<Animator>();
		Anim.enabled = true;
	//  look for child P1_Head and put it in an empty gameObject
	//	look for the spritrenderer array HeadSprite
		Head = transform.FindChild("P1_Head").gameObject;
       	Head.GetComponent<SpriteRenderer>().sprite = HeadSprite[0];
		Muzzle = transform.FindChild("P1_Muzzle").gameObject;	
		Muzzle.GetComponent<Renderer>().enabled = false;
		PowerUpScore = 0;
		PowerUpbar.NewValue = PowerUpScore;
		PlayerLives = 3;

					
		laser = GetComponent<AudioSource>();
	}


	void Die ()
	{
		Instantiate (PlayerClone, transform.position, Quaternion.identity);	
		Destroy (gameObject);
		} 


	void Fire ()
	{
// Power Up system 
		if (PowerUpScore == 0) {
		// createt offset for bullet on a new vector3
			Vector3 newPos = new Vector3 (0, 0.1f, 0);

			// create a gameobject that instantiate the bullet on the gamescene
			GameObject beam = Instantiate (Bullet [0], this.transform.position + newPos, Quaternion.identity) as GameObject;
			// getting the position of beam, edit the y position by getting the old y position and decrease it by 0.4f and store it back as its new position.
			//GameObject beam = Instantiate (Bullet, this.transform.position, Quaternion.identity) as GameObject;			// make the bullet go downwards by accessing the Rigidbody and ad a new Vector 3 with var Y
			beam.GetComponent <Rigidbody2D> ().velocity = new Vector3 (0, -12);
			// if it is not lower than 3, instantiate the power up (this method is not effective when using multiple power-ups)
		} 
			
		else if (PowerUpScore == 1) {
			Vector3 newPos = new Vector3 (0, 0.1f, 0);
			GameObject beam = Instantiate (Bullet [0], this.transform.position + newPos, Quaternion.identity) as GameObject;
			GameObject beam2 = Instantiate (Bullet [3], this.transform.position + newPos, Quaternion.identity) as GameObject;
			beam.GetComponent <Rigidbody2D> ().velocity = new Vector3 (0, -12);
			beam2.GetComponent <Rigidbody2D> ().velocity = new Vector3 (0, 12);
		}

		else if (PowerUpScore == 2) {
			Vector3 newPos = new Vector3 (0, 0.1f, 0);
			GameObject beam = Instantiate (Bullet [1], this.transform.position + newPos, Quaternion.identity) as GameObject;
			GameObject beam2 = Instantiate (Bullet [3], this.transform.position + newPos, Quaternion.identity) as GameObject;
			beam.GetComponent <Rigidbody2D> ().velocity = new Vector3 (0, -12);
			beam2.GetComponent <Rigidbody2D> ().velocity = new Vector3 (0, 12);

		}

		

		else if (PowerUpScore >= 3) {
			Vector3 newPos = new Vector3 (0, 0.3f, 0);
			GameObject beam = Instantiate (Bullet [2], this.transform.position + newPos, Quaternion.identity) as GameObject;
			GameObject beam2 = Instantiate (Bullet [2], this.transform.position + newPos, Quaternion.identity) as GameObject;
			GameObject beam3 = Instantiate (Bullet [1], this.transform.position + newPos, Quaternion.identity) as GameObject;
			GameObject beam4 = Instantiate (Bullet [3], this.transform.position + newPos, Quaternion.identity) as GameObject;
			beam.transform.Rotate (0,0,-45);
			beam.GetComponent <Rigidbody2D>().velocity = new Vector3 (-8, -8) + transform.forward;
			beam2.transform.Rotate (0,0,45);
			beam2.GetComponent <Rigidbody2D>().velocity = new Vector3 (8, -8) + transform.forward;
			beam3.GetComponent <Rigidbody2D> ().velocity = new Vector3 (0, -12);
			beam4.GetComponent <Rigidbody2D> ().velocity = new Vector3 (0, 8);



		}
		
		StartCoroutine(MuzzleToggle());
		StartCoroutine(HeadAnimation());
	//Play the laser sound fx
		laser.Play();
	}
	
	
	void OnTriggerEnter2D (Collider2D Col)
	{	// return value if a collission with projectile is detected 

		if (Col.gameObject.tag != ("Item")) {	
		
			PlayerLives--;	
			GameObject Clone = Instantiate (PlayerHitFX, this.transform.position, Quaternion.identity) as GameObject;
			Destroy (Clone, 2f);
			AudioSource.PlayClipAtPoint (PlayerHitClip, Camera.main.transform.position);
			StartCoroutine (PlayerHit ());
			StartCoroutine (SpriteHitColor ());	
			if (PowerUpScore != 0) {
				PowerUpbar.NewValue -= 1;
				PowerUpScore -= 1;
			}

			if (PowerUpScore <= 0) {
				PowerUpScore = 0;
				PowerUpbar.NewValue = PowerUpbar.MinValue;
			}

		}
	
		if (PlayerLives >= 3) {
			PlayerLives = 3;
			PlayerLives += 0;
		}

		if (PlayerLives <= 0) {
				Die();
		
			}

		}

	
	IEnumerator MuzzleToggle ()
	{
		Muzzle.GetComponent<Renderer>().enabled = true;
		yield return new WaitForSeconds (0.1f);
		Muzzle.GetComponent<Renderer>().enabled = false;
	}

	IEnumerator HeadAnimation()
	{	//again look for the head gameobject and change it's sprites
		Head.GetComponent<SpriteRenderer>().sprite = HeadSprite[1];
		yield return new WaitForSeconds(0.1F);
		Head.GetComponent<SpriteRenderer>().sprite = HeadSprite[2];
		yield return new WaitForSeconds(0.1F);
		Head.GetComponent<SpriteRenderer>().sprite = HeadSprite[0];
	}
//turns collider off and on when hit to prevent instant kill
	IEnumerator PlayerHit ()
	{
	//GetComponent<PolygonCollider2D>().enabled = false;
	gameObject.layer = 17;
	yield return new WaitForSeconds(2.5f);
	gameObject.layer = 10;
	//GetComponent<PolygonCollider2D>().enabled = true;
	}

	IEnumerator SpriteHitColor ()
	{
		GetComponent<SpriteRenderer>().color = Color.red;
		Head.GetComponent<SpriteRenderer>().color = Color.red;
		yield return new WaitForSeconds(0.1F);
		GetComponent<SpriteRenderer>().color = Color.white;
		Head.GetComponent<SpriteRenderer>().color = Color.white;
		yield return new WaitForSeconds(0.1F);
		GetComponent<SpriteRenderer>().color = Color.red;
		Head.GetComponent<SpriteRenderer>().color = Color.red;
		yield return new WaitForSeconds(0.1F);
		GetComponent<SpriteRenderer>().color = Color.white;
		Head.GetComponent<SpriteRenderer>().color = Color.white;
		yield return new WaitForSeconds(0.1F);
		GetComponent<SpriteRenderer>().color = Color.red;
		Head.GetComponent<SpriteRenderer>().color = Color.red;
		yield return new WaitForSeconds(0.1F);
		GetComponent<SpriteRenderer>().color = Color.white;
		Head.GetComponent<SpriteRenderer>().color = Color.white;
		yield return new WaitForSeconds(0.1F);
		GetComponent<SpriteRenderer>().color = Color.red;
		Head.GetComponent<SpriteRenderer>().color = Color.red;
		yield return new WaitForSeconds(0.1F);
		GetComponent<SpriteRenderer>().color = Color.white;
		Head.GetComponent<SpriteRenderer>().color = Color.white;
	}	

	// Update is called once per frame
	void Update ()
	{

		if (this.gameObject == null) {
			return;
		}

		{
			Destroy(Anim, 1.1f);
			
			// easy x & y acces
			transform.position += new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"), 0.0f) * speed * Time.deltaTime;

		}

		// vector 3 pos from the cameraview to the transform of object
		Vector3 pos = Camera.main.WorldToViewportPoint (transform.position);

		// clamp the position on x & y
		pos.x = Mathf.Clamp (pos.x, 0.04f, 0.96f);
		pos.y = Mathf.Clamp (pos.y, 0.04f, 0.93f);
		
		// restrict the object in the playspace of the camview (with the clamp attached)
		transform.position = Camera.main.ViewportToWorldPoint (pos);
		
		//ShipMovement position of this gameobject += (add to) Vector3.left * public float speed of value in Time.deltaTime; 
		
		if (Input.GetKey (KeyCode.A)) {
			//this.transform.rotation = Quaternion.AngleAxis (3, Vector3.forward);
		 GetComponent<SpriteRenderer>().sprite = Ship[1];
		
		} else if (Input.GetKey (KeyCode.D)) {
			//this.transform.rotation = Quaternion.AngleAxis (-5, Vector3.forward);
		 GetComponent<SpriteRenderer>().sprite = Ship[2];

		}

		if (Input.GetKeyUp (KeyCode.A)) {
			//this.transform.rotation = Quaternion.AngleAxis (0, Vector3.forward);
		 GetComponent<SpriteRenderer>().sprite = Ship[0];

		
		} else if (Input.GetKeyUp (KeyCode.D)) {
			//this.transform.rotation = Quaternion.AngleAxis (0, Vector3.forward);
		 GetComponent<SpriteRenderer>().sprite = Ship[0];

		}
	
		if (Input.GetKeyDown (KeyCode.RightShift)) {
			//for a cooldown use InvokeRepeating, GameObject Fire Time, RepeatRate (Cooldown)
			InvokeRepeating ("Fire", 0.001f, BulletDelay);
		}
				//StartCoroutine (HeadAnimation.ChangeFace());		
			
		 else if (Input.GetKeyUp (KeyCode.RightShift)) {
			CancelInvoke ("Fire");
			Head.GetComponent<SpriteRenderer>().sprite = HeadSprite[0];

		}

	}
}
 
