using UnityEngine;
using System.Collections;

public class Life : MonoBehaviour {
public AudioClip LifePickUp;
public GameObject LifePickUpFX;
private SpriteRenderer Sprite;


	void Start ()
	{
		Sprite = GetComponentInChildren<SpriteRenderer>();
		StartCoroutine (SpriteFlicker());

	}

	void OnTriggerEnter2D (Collider2D Col)
	{
	Player1Controller.PlayerLives++;

	AudioSource.PlayClipAtPoint (LifePickUp, Camera.main.transform.position);
	GameObject Clone = Instantiate (LifePickUpFX, this.transform.position, Quaternion.identity) as GameObject;
	Destroy (Clone, 2f);

	Destroy (this.gameObject);

		if (Player1Controller.PlayerLives >=3)
			Player1Controller.PlayerLives = 3;
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
