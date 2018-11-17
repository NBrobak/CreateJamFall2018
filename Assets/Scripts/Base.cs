using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour {
/*************/
/* Variables */
/*************/
	[SerializeField]
	private int point;
	//struct med baby obj med xy floats
	private struct StationaryBaby
	{
		public GameObject baby;
		public float xPos;
		public float yPos;
	}
	private StationaryBaby[] stationaryLoc;
	private Player masterBaby;
/**************/
/* Properties */
/**************/
	public int Point{
		get{
			return this.point;
			}
		set{
			if(value >= 0)
				this.point = value;
		}
	}

	public Player MasterBaby
	{
		get { return masterBaby; }
		private set { masterBaby = value; }
	}

	// Use this for initialization
	void Start () {
		stationaryLoc = new StationaryBaby[5];
		for (int i = 0; i < stationaryLoc.Length; i++)
		{
			stationaryLoc[i].baby = null;
			stationaryLoc[i].xPos = this.transform.GetChild(0).GetComponent<Transform>().position.x;
			stationaryLoc[i].yPos = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}


/*************/
/* Functions */
/*************/
	public void Respawn(){

	}
	public void Allocate(List<FollowerBaby> babies){
		if(babies != null){
			for(int i = 0; i < 4; i++){
				if(stationaryLoc[i].baby == null){
					
				}
			}
		}
	}
}
