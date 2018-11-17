using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
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

    public int Point
    {
        get
        {
            return this.point;
        }
        set
        {
            if (value >= 0)
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
        stationaryLoc = this.gameObject.GetComponentsInChildren<Transform>();
        Debug.Log("Array length: " + stationaryLoc.Length);
        foreach (var item in stationaryLoc)
        {
            Debug.Log(item.position);
        }
        stationaryBabiesPos = new StationaryBaby[stationaryLoc.Length - 1];

        for (int i = 1; i < stationaryBabiesPos.Length; i++)
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
            for (int i = 1; i < stationaryBabiesPos.Length; i++)
            {
                if (!stationaryBabiesPos[i].isOccupied && babies.Count != 0)
                {
                    stationaryBabiesPos[i].followerBaby = babies[0];
                    babies[0].targetPos = new Vector3(stationaryBabiesPos[i].xPos, 1f, stationaryBabiesPos[i].yPos);
                    babies[0].targetPos.y = 1f;
                    babies[0].isBased = true;
                    babies.RemoveAt(0);
                }
            }
            if (babies.Count > 0)
            {
                foreach (var item in babies)
                {
                    Destroy(item);
                }
            }
            Debug.Log(babies.Count);

            //babies.Clear();
        }
    }

    public void LosePoint()
    {
        point--;
    }
    public void GainPoint()
    {
        point++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (this.gameObject.name.Contains(player.playerName))
            {
                Allocate(player.Babies);
            }
        }
    }
}
