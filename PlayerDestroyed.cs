using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerDestroyed : MonoBehaviour {
public float EndTime = 4f;
public Sprite[] HeadSprite;
private GameObject Head;

	public void LoadLevel (string name)
	{
		SceneManager.LoadScene(name);
	}


	// Use this for initialization
	void Update ()
	{
		StartCoroutine(EndGame());
	}
	
	IEnumerator EndGame(){
		yield return new WaitForSeconds (EndTime);
		LoadLevel("EndScreen");
		
	}


	


}
