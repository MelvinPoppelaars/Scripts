using UnityEngine;
using System.Collections;

public class HeadAnimation : MonoBehaviour {
public Sprite[] mySprites;

private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start ()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		}
	

	public IEnumerator FaceLoop() {
		spriteRenderer.sprite = mySprites[1];	
	//Yield return new overwrites 
        yield return new WaitForSeconds(1f);
		spriteRenderer.sprite = mySprites[2];
		yield return new WaitForSeconds(1f);
		spriteRenderer.sprite = mySprites[0];
    }

}
