using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
public AudioClip ItemPickUp;
public GameObject ItemPickUpFX;
private SpriteRenderer Sprite;


	void Start ()
	{
		Sprite = GetComponentInChildren<SpriteRenderer>();
		StartCoroutine (SpriteFlicker());

	}

	void OnTriggerEnter2D (Collider2D Col)
	{

	if (Player1Controller.PowerUpScore != 3) {
			PowerUpbar.NewValue ++;
			Player1Controller.PowerUpScore ++;
			}
	

	AudioSource.PlayClipAtPoint (ItemPickUp, Camera.main.transform.position);
	GameObject Clone = Instantiate (ItemPickUpFX, this.transform.position, Quaternion.identity) as GameObject;
	Destroy (Clone, 2f);

	Destroy (this.gameObject);

	}


	IEnumerator SpriteFlicker() {
		yield return new WaitForSeconds (6F);
		Sprite.enabled = false;
		yield return new WaitForSeconds (0.25F);
		Sprite.enabled = true;
		yield return new WaitForSeconds (0.25F);
		Sprite.enabled = false;
		yield return new WaitForSeconds (0.15F);
		Sprite.enabled = true;
		yield return new WaitForSeconds (0.15F);
		Sprite.enabled = false;
		yield return new WaitForSeconds (0.10F);
		Sprite.enabled = true;
		yield return new WaitForSeconds (0.10F);
		Sprite.enabled = false;
		yield return new WaitForSeconds (0.05F);
		Sprite.enabled = true;
		yield return new WaitForSeconds (0.05F);
		Sprite.enabled = false;
		yield return new WaitForSeconds (0.05F);
		Sprite.enabled = true;
		yield return new WaitForSeconds (0.05F);
		Sprite.enabled = false;
		yield return new WaitForSeconds (0.05F);
		Sprite.enabled = true;
		Destroy (gameObject);


	}

}
