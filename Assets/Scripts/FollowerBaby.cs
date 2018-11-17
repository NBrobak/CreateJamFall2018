using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerBaby : MonoBehaviour {
	GameObject Player;
	private bool isbased;

	public FollowerBaby(GameObject PlayerObj, bool _isbased, Vector3 _spawnPos)
	{
		this.Player = PlayerObj;
		this.isbased = _isbased;
		this.transform.position = _spawnPos;
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
