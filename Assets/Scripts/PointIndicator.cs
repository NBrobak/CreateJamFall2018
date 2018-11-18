using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointIndicator : MonoBehaviour {
	public Base baseRef;
	public Camera cameraRef;
	private TextMesh scoreText;
	// Use this for initialization
	void Start () {
		scoreText = transform.GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(cameraRef.transform.position, Vector3.up);
		scoreText.text = baseRef.Point.ToString();
	}
}
