using UnityEngine;
using System.Collections;

public class dummy : MonoBehaviour {
float speed = 1.0f;


	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f * speed * Time.deltaTime);
		// assign vector 3 pos from the cameraview to the transform of object
	    Vector3 pos = Camera.main.WorldToViewportPoint (transform.position);
        // clamp the position on x & y
		 pos.x = Mathf.Clamp01(pos.x);
         pos.y = Mathf.Clamp01(pos.y);
		// restrict the object in the playspace of the camview (with the clamp attached)
         transform.position = Camera.main.ViewportToWorldPoint(pos);
	}
}
