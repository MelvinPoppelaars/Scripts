using UnityEngine;
using System.Collections;

public class IdleState : MonoBehaviour {
public AudioClip BulletHit;
public int ColliderIndicator;
	// Use this for initialization
	void Start () {

	}
	
	void OnTriggerEnter2D (Collider2D Col)
	{

		if (ColliderIndicator == 1) {
			AudioSource.PlayClipAtPoint (BulletHit, Camera.main.transform.position);
		}
		
		if (ColliderIndicator == 2) {
			projectile Missile = Col.gameObject.GetComponent<projectile> ();
			if (Missile) {
			StatePatterns.Health -= Missile.GetDamage();
			BossBar.NewBossValue -= Missile.GetDamage();
			StartCoroutine(SpriteHit());
			AudioSource.PlayClipAtPoint (BulletHit, Camera.main.transform.position);
			
			}
		
		}
	

	}

	IEnumerator SpriteHit() {
	transform.parent.GetComponent<SpriteRenderer>().color = Color.red;
	yield return new WaitForSeconds (0.1f);
	transform.parent.GetComponent<SpriteRenderer>().color = Color.white;

	}

	// Update is called once per frame
	void Update () {
	}
}
