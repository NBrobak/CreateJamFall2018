using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField]
    private int point = 0;
    private bool isInSpawn = false;
    private List<FollowerBaby> BabyScoreVisuals;

    private struct StationaryBaby
    {
        public float xPos;
        public float yPos;
        public bool isOccupied;
    }

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
        set { masterBaby = value; }
    }

    private StationaryBaby[] spawnPoints = new StationaryBaby[5];
    private List<Vector3> spawnPointsVector3;

    // Use this for initialization
    void Start()
    {
        BabyScoreVisuals = new List<FollowerBaby>();
        spawnPointsVector3 = new List<Vector3>();
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i].xPos = this.gameObject.transform.GetChild(i).transform.position.x;
            spawnPoints[i].yPos = this.gameObject.transform.GetChild(i).transform.position.z;
            spawnPoints[i].isOccupied = false;
            spawnPointsVector3.Add(new Vector3(spawnPoints[i].xPos, 1, spawnPoints[i].yPos));
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
        Debug.Log("Allocate method called");
        if (babies != null)
        {
            while (spawnPointsVector3.Count > 0)
            {
                if (babies.Count > 0)
                {
                    GainPoint();
                    BabyScoreVisuals.Add(babies[0]);
                    int randomElement = Random.Range(0, spawnPointsVector3.Count - 1);
                    babies[0].isBased = true;
                    babies[0].targetPos = spawnPointsVector3[randomElement];
                    spawnPointsVector3.RemoveAt(randomElement);
                    babies.RemoveAt(0);
                }
                else
                {
                    break;
                }
            }

            if (babies.Count > 0)
            {
                while (babies.Count > 0)
                {
                    FollowerBaby temp = babies[0];
                    babies.RemoveAt(0);
                    Destroy(temp.gameObject);
                    GainPoint();
                }



                // foreach (var item in babies)
                // {
                //     GainPoint();
                //     Destroy(item.gameObject);
                // }
            }

        }



        //     if (babies != null)
        //     {
        //         point += babies.Count;
        //         for (int i = 0; i < stationaryBabiesPos.Length; i++)
        //         {
        //             if (babies.Count != 0)
        //             {
        //                 if (!stationaryBabiesPos[i].isOccupied)
        //                 {
        //                     stationaryBabiesPos[i].followerBaby = babies[0];
        //                     stationaryBabiesPos[i].isOccupied = true;
        //                     babies[0].targetPos = new Vector3(stationaryBabiesPos[i].xPos, 1f, stationaryBabiesPos[i].yPos);
        //                     babies[0].isBased = true;
        //                     babies.RemoveAt(0);
        //                 }
        //             }
        //         }
        //         if (babies.Count > 0)
        //         {
        //             for (int i = 0; i < babies.Count; i++)
        //             {
        //                 babies[i].destroySelf();
        //             }
        //         }
        //         Debug.Log(babies.Count);

        //         //babies.Clear();
        //     }
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
        Player player = other.GetComponent<Player>();
        if (player != null && player == MasterBaby)
        {
            if (isInSpawn == false)
            {
                isInSpawn = true;
                Allocate(player.Babies);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null && player == MasterBaby)
        {
            isInSpawn = false;
        }
    }
}
