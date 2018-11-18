using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAtCamera : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(2f * transform.position - Camera.main.transform.position);
	}
}
