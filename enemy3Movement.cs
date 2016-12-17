using UnityEngine;
using System.Collections;

public class enemy3Movement : MonoBehaviour {

private float xMin;
private float xMax;
private static bool MovingRight = false;
public float Speed;



	// Use this for initialization
	void Start () {

		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		//create a new vector3 edge of left and right side 
		Vector3 LeftEdge = Camera.main.ViewportToWorldPoint (new Vector3 (0,0, distanceToCamera));
		Vector3 RightEdge = Camera.main.ViewportToWorldPoint (new Vector3 (1,0, distanceToCamera));
		//assign the new edge on the x position in the float xMin and xMax
		xMin = LeftEdge.x;
		xMax = RightEdge.x;

	}

	void OnDrawGizmos() {

		Gizmos.DrawWireCube (transform.position, new Vector3 (4,2));
	}
	
	// Update is called once per frame
void Update ()
	{
		// creates movement for when MovingRight = true
		if (MovingRight) {
			transform.position += Vector3.right * Speed * Time.deltaTime;
			// creates movement for when MovringRight = false
		} else {
			transform.position += Vector3.left * Speed * Time.deltaTime;
		}
			// store a float for when the edges are reached
		float LeftEdgeFormation = transform.position.x - 0.5f * 4F;
		float RightEdgeFormation = transform.position.x + 0.5f * 4F;

			if (LeftEdgeFormation < xMin) {
				MovingRight = true;
			} 
			
			if (RightEdgeFormation > xMax) {
				MovingRight = false;
			}		

		}

	}
