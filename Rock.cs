using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour {
public float speed = 5f;
int scorevalue = 10;
public GameObject RockFX;
public GameObject[] Item;
private float probability = 0.5F;
private float ItemSpawn;
private int SpawnItem;
public AudioClip BulletImpact;

	// Use this for initialization
	void Start ()
	{
	
		ItemSpawn = Random.value;
		SpawnItem = Random.Range (0, Item.Length );

	}

	
	// Update is called once per frame
	void Update () {
	transform.position += Vector3.up * speed * Time.deltaTime;
	

	}

	void OnTriggerEnter2D (Collider2D Col)
	{	
		//when a trigger on the collider is detected, look if the collision is triggered by the collision of the projectilve component
		projectile missile = Col.gameObject.GetComponent<projectile> ();
		if (missile) {
			AudioSource.PlayClipAtPoint (BulletImpact, Camera.main.transform.position);
			if (RockFX != null) {
				GameObject Clone = Instantiate (RockFX, this.transform.position, Quaternion.identity) as GameObject;
				Destroy(Clone, 2f);
			}
			Destroy (this.gameObject); 	
			ScoreManager.score += scorevalue;

			if (ItemSpawn >= probability) {
				Instantiate (Item[SpawnItem], transform.position,Quaternion.identity);
		}				

		}
	}
}