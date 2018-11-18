using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Base : MonoBehaviour
{
    [SerializeField]
    private float respawnTime = 2;
    [SerializeField]
    private int point = 0;
    private List<FollowerBaby> BabyScoreVisuals;

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

    private List<Vector3> spawnPointsVector3;
    private bool isFenceUP = false;
    private Transform fence;
    private float fenceMoveSpeed = 6f;

    void Start()
    {
        BabyScoreVisuals = new List<FollowerBaby>();
        spawnPointsVector3 = new List<Vector3>();
        Transform[] childTransforms = transform.Cast<Transform>().Where(c => c.gameObject.tag == "BaseSpawnPos").ToArray();
        for (int i = 0; i < childTransforms.Length; i++)
        {
            Transform spawnPoint = childTransforms[i];

            spawnPointsVector3.Add(new Vector3(spawnPoint.position.x, 1, spawnPoint.position.z));
        }
        spawnPointsVector3.Shuffle();
        fence = transform.GetChild(5).transform;
    }

    void Update()
    {
        if (isFenceUP)
        {
            fence.position = Vector3.MoveTowards(fence.position, new Vector3(fence.position.x, -1.377f, fence.position.z), fenceMoveSpeed * Time.deltaTime);
        }
        else
        {
            fence.position = Vector3.MoveTowards(fence.position, new Vector3(fence.position.x, -2.56f, fence.position.z), fenceMoveSpeed * Time.deltaTime);
        }
    }

    public void Respawn()
    {
        masterBaby.transform.position = transform.position;
        masterBaby.Life = Player.MAX_HEALTH;
        masterBaby.enabled = false;
        StartCoroutine(ReactivatePlayer());
    }

    private IEnumerator ReactivatePlayer()
    {
        //put up fence
        isFenceUP = true;
        // fence.new Vector3(transform.GetChild(5).transform.position.x, -1.377f, transform.GetChild(5).transform.position.z);
        yield return new WaitForSeconds(respawnTime);
        //take down fence
        isFenceUP = false;
        // transform.GetChild(5).transform.position = new Vector3(transform.GetChild(5).transform.position.x, -2.56f, transform.GetChild(5).transform.position.z);
        masterBaby.enabled = true;
    }

    public void Allocate(List<FollowerBaby> babies)
    {
        Debug.Log("Allocate method called");
        if (babies != null)
        {
            babies.Reverse();
            foreach (FollowerBaby baby in babies)
            {
                if (Point < spawnPointsVector3.Count)
                {
                    BabyScoreVisuals.Add(baby);
                    baby.MoveToBase(spawnPointsVector3[Point], true);
                }
                else
                {
                    baby.MoveToBase(spawnPointsVector3[0], false);
                }
                GainPoint();
            }
        }
        babies.Clear();
    }

    public void LosePoint()
    {
        Point--;
    }
    public void GainPoint()
    {
        Point++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player == MasterBaby)
            {
                Allocate(player.Babies);
            }
        }
    }
}
