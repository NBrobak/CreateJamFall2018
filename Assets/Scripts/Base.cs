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
    public AudioClip respawnSound;
    private List<FollowerBaby> BabyScoreVisuals;
    private AudioSource audioSrc;
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
    public Transform fence;
    public float fenceMoveSpeed = 0.3f;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
            BabyScoreVisuals = new List<FollowerBaby>();
        spawnPointsVector3 = new List<Vector3>();
        Transform[] childTransforms = transform.Cast<Transform>().Where(c => c.gameObject.tag == "BaseSpawnPos").ToArray();
        for (int i = 0; i < childTransforms.Length; i++)
        {
            Transform spawnPoint = childTransforms[i];

            spawnPointsVector3.Add(new Vector3(spawnPoint.position.x, 1, spawnPoint.position.z));
        }
        spawnPointsVector3.Shuffle();
    }

    void Update()
    {
        if (isFenceUP)
        {
            Debug.Log("Fence is actually being put up");
            fence.position = Vector3.MoveTowards(fence.position, new Vector3(fence.position.x, 1.377f, fence.position.z), fenceMoveSpeed * Time.deltaTime);
        }
        else
        {
                   Debug.Log("Fence is actually being put down");
            fence.position = Vector3.MoveTowards(fence.position, new Vector3(fence.position.x, -1f, fence.position.z), fenceMoveSpeed * Time.deltaTime);
        }
    }

    public void Respawn()
    {
        audioSrc.clip = respawnSound;
        audioSrc.volume = .2f;
        audioSrc.pitch = Random.Range(.8f, 1.2f);
        audioSrc.Play();
        masterBaby.transform.position = transform.position;
        masterBaby.Life = Player.MAX_HEALTH;
        masterBaby.enabled = false;
        isFenceUP = true;
        Debug.Log("Fence is being put up");
        StartCoroutine(ReactivatePlayer());
    }

    private IEnumerator ReactivatePlayer()
    {
        // fence.new Vector3(transform.GetChild(5).transform.position.x, -1.377f, transform.GetChild(5).transform.position.z);
        yield return new WaitForSeconds(respawnTime);
        //take down fence
        isFenceUP = false;
        Debug.Log("Fence is being put down");
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
