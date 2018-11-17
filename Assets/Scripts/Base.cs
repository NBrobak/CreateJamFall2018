using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour {
	[SerializeField]
	private int point = 0;
	private struct StationaryBaby
	{
		public FollowerBaby followerBaby;
		public float xPos;
		public float yPos;
		public bool isOccupied;
	}

    private Transform[] stationaryLoc;
    private StationaryBaby[] stationaryBabiesPos;

    private Player masterBaby;

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
    void Start()
    {
        stationaryLoc = this.GetComponentsInChildren<Transform>();
        stationaryBabiesPos = new StationaryBaby[stationaryLoc.Length];

        for (int i = 0; i < stationaryBabiesPos.Length; i++)
        {
            stationaryBabiesPos[i].xPos = stationaryLoc[i].position.x;
            stationaryBabiesPos[i].yPos = stationaryLoc[i].position.z;
            stationaryBabiesPos[i].isOccupied = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Respawn()
    {
        masterBaby.gameObject.transform.position = this.gameObject.transform.position;
    }

    public void Allocate(List<FollowerBaby> babies)
    {
        if (babies != null)
        {
            point += babies.Count;
            for (int i = 0; i < stationaryBabiesPos.Length; i++)
            {
                if (!stationaryBabiesPos[i].isOccupied && babies.Count != 0)
                {
                    stationaryBabiesPos[i].followerBaby = babies[0];
                    babies[0].targetPos.x = stationaryBabiesPos[i].xPos;
                    babies[0].targetPos.z = stationaryBabiesPos[i].yPos;
                    babies[0].targetPos.y = 0f;
                    babies.RemoveAt(0);
                }
            }
            babies.Clear();
        }
    }

	public void LoosePoint(){

	}
	public void GainPoint(){
		
	}
}
