using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour {
private List <GameObject> Characters;
private int SelectionIndex =0;
public AudioClip Switch;
public AudioClip Selected;
public int StartDelay;
public GameObject Player1;
private Animator anim1;
public GameObject Player2;
private Animator anim2;
private bool SelectMode;
private GameObject StartTxt;
private GameObject CharacterDeselect;
	// Use this for initialization

public void loadlevel (string name)
	{
			SceneManager.LoadScene (name);
	}

	void Start ()
	{
		StartTxt = GameObject.Find("Start");
		CharacterDeselect = GameObject.Find("Characters");
		SelectMode = true;
		anim1 = Player1.GetComponent<Animator>();
		anim2 = Player2.GetComponent<Animator>();
		
		
	// get the int that was saved from previous scene
	SelectionIndex = PlayerPrefs.GetInt("CharacterSelected");



	// Creates a new list for every transform of the children in this object and set the appearance to false
		Characters = new List <GameObject> ();

		foreach (Transform t in transform) {
		
			Characters.Add (t.gameObject);
			t.gameObject.SetActive (false);
			
		}
	// Set the character that matches the SelectionIndex to true
		Characters[SelectionIndex].SetActive (true);
	}
	
	// creates an int Index == SelectionIndex where if SelectionIndex is the same as Index will turn true;
	public void Select (int Index)
	{
		if (Index == SelectionIndex)
			return;

		Characters[SelectionIndex].SetActive(false);
		SelectionIndex = Index;
		Characters[SelectionIndex].SetActive(true);

		
	}

	// Update is called once per frame
	void Update ()
	{	
		Characters [SelectionIndex].SetActive (true);

		if (Characters [SelectionIndex] != Characters [SelectionIndex]) {
			Characters [SelectionIndex].SetActive (false);

		}
		// Keys only work when it is on the menu scene
		if (SceneManager.GetActiveScene ().name == "Start") {
			// on the input we select the number of the int Index by a keypress to select the character

			if (Input.GetKeyDown (KeyCode.LeftArrow) && SelectionIndex != 0 &&  SelectMode == true) {
				AudioSource.PlayClipAtPoint (Switch, Camera.main.transform.position);
				Select (0);
		
			
			}
		
			if (Input.GetKeyDown (KeyCode.RightArrow) && SelectionIndex != 1 &&  SelectMode == true) {
				AudioSource.PlayClipAtPoint (Switch, Camera.main.transform.position);
				Select (1);
			}

			if (Input.GetKeyDown (KeyCode.Space)) {
				if (SelectionIndex == 0) {
					StartCoroutine (StartGameP1 ());
				}

				if (Input.GetKeyDown (KeyCode.Space)) {
					if (SelectionIndex == 1) {
						StartCoroutine (StartGameP2 ());
					}
				}
			}
	
		}
	}

		IEnumerator StartGameP1() {
		CharacterDeselect.SetActive(false);
		StartTxt.SetActive(false);
		SelectMode = false;
		anim1.Play("P1Selected");
		AudioSource.PlayClipAtPoint(Selected, Camera.main.transform.position);
		yield return new WaitForSeconds (StartDelay);
		PlayerPrefs.SetInt ("CharacterSelected", SelectionIndex);
		loadlevel ("Game");
		
	}

		IEnumerator StartGameP2() {
		CharacterDeselect.SetActive(false);
		StartTxt.SetActive(false);
		SelectMode = false;
		anim2.Play("P2_Selected");
		AudioSource.PlayClipAtPoint(Selected, Camera.main.transform.position);
		yield return new WaitForSeconds (StartDelay);
		PlayerPrefs.SetInt ("CharacterSelected", SelectionIndex);
		loadlevel ("Game");
		
	}

}



