using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public float speed = 5;
    public GameObject bone;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        bone.transform.Rotate(Vector3.up * (speed * Time.deltaTime), Space.Self);
	}
}
