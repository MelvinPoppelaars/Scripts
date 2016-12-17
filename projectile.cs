using UnityEngine;
using System.Collections;

public class projectile : MonoBehaviour {

public float damage = 100f;
public float bulletspeed = 1f;
public GameObject HitParticle;
private GameObject Clone;
public AudioClip DeathExplosion;
private GameObject Player1;
//Will return the damage we deal


void Start ()
	{
// try to make the bullet explode only when it has collision with the player
	//	Player1 = GameObject.Find ("Player1");
	//	PlayerCol = Player1.GetComponent<Collider2D> ();		
}

public float GetDamage (){
		return(damage);
}


//signal it has been hit, destroy on hit
void OnTriggerEnter2D (Collider2D col)
	{
// store the particle object in a new gameObject so we can destroy that gameObject after time
		GameObject Clone = Instantiate (HitParticle, this.transform.position, Quaternion.identity) as GameObject;
		Destroy (Clone, 4f);

		if (DeathExplosion != null) {
	//		AudioSource.PlayClipAtPoint (DeathExplosion, Camera.main.transform.position);
		}
		Destroy(this.gameObject);
	
							
	}

}
