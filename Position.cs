using UnityEngine;
using System.Collections;

public class Position : MonoBehaviour {

private int amount;
private GameObject[] enemies;
public static bool Full = false;
public static int FullCount = 0;



// (transform, position in worldpace, radius of sphere
		void OnDrawGizmos ()
	{
		Gizmos.DrawWireSphere (transform.position, 0.5f);
	}

}
		
