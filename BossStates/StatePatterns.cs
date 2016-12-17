using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatePatterns : MonoBehaviour {

private Sprite sprite;
public float Delay;
public GameObject[] Lights;
public static bool Attack1 = false;
public static bool Attack2 = false;
public Collider2D IdleCol;
public Collider2D Attack1Middle;
public Collider2D Attack2Left;
public Collider2D Attack2Right;
public GameObject FinalScore;
public static float Health = 20000;
public GameObject FireWorks;
public GameObject FinalBossDestroyed;

enum States {
IDLE = 0,
ATTACK1 = 1,
ATTACK2 = 2,
ATTACK3 = 3,
	}
	
	States CurrentState = States.IDLE;

private Attack1State attack1State;
private Attack2State attack2State;


	// Use this for initialization
	void Awake () {
	attack1State = GetComponent<Attack1State>();
	attack2State = GetComponent<Attack2State>();

//		sprite = GetComponent<SpriteRenderer>().sprite;
	}

	void Start ()
	{
// Switching scripts in from the ChangeState method
	InvokeRepeating("Switch", 0f, Delay);
	}

		void Switch()
	{
		ChangeState(CurrentState);
	}

		void Update ()
	{
		if (Health <= 0) {
			Instantiate (FinalBossDestroyed, this.transform.position, Quaternion.identity);
			Destroy(this.gameObject);
			
			transform.position = new Vector3 (0,0,0);
			FinalScore.SetActive (true);
			Instantiate (FireWorks, transform.position, Quaternion.identity);
			GameObject.Find("Canvas").SetActive(false);
			GameObject.Find("FinalBossBar").SetActive(false);
			GameObject.Find("Rockspawners").SetActive(false);

		
		}
	}
// Define the states switch to turn on / off scripts
	void ChangeState (States NewState)
	{		

		CurrentState = (States)Random.Range (0,4);

		switch (NewState) {
		
		case States.IDLE:
			{
				
				Lights[0].SetActive(false);
				Lights[1].SetActive(false);

				Attack1Middle.enabled = false;
				Attack2Left.enabled = false;
				Attack2Right.enabled = false;

				attack1State.enabled = false;
				attack2State.enabled = false;
			
				break;
			}

		case States.ATTACK1:
			{
				Lights[0].SetActive(true);
				Lights[1].SetActive(false);

				Attack1Middle.enabled = true;
				Attack2Left.enabled = false;
				Attack2Right.enabled = false;



				Attack1 = true;
				attack1State.enabled = true;
				attack2State.enabled = false;
				
				break;
			}
		case States.ATTACK2:
			{
				Lights[0].SetActive(false);
				Lights[1].SetActive(true);

				Attack1Middle.enabled = false;
				Attack2Left.enabled = true;
				Attack2Right.enabled = true;


				Attack2 = true;
				attack1State.enabled = false;
				attack2State.enabled = true;
				
				break;
			}
		
		default:
			{
				return;
			}
		}
	}


}
