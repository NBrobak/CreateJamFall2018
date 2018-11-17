using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    //bullet variables
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    


    //grenade Variables
    public GameObject atheistGrenadeBabyPrefab;
    private GameObject atheistGrenadeBaby;
    public float timer;
    public float range;
    public float speed = 3.0f;
    public float damage;
    private Animator animator;
    public Transform grenadeSpawn;

    public FollowerBaby BabyFollowerPrefab;
    public float moveSpeed;
    public float reboundTime = 0.3f;
    public string playerName;
    public Color playerColor;
    private int life = 5;

    [HideInInspector]
    public List<FollowerBaby> Babies;
    private float cooldown;
    private Base personalBase;
    private Rigidbody playersRigidbody;
    private bool hasFiredShot = false;
    private bool hasFiredGrenade = false;

    private float Cooldown
    {
        get
        {
            return this.cooldown;
        }
        set
        {
            if (value > 0f)
            {
                this.cooldown = value;
            }
        }
    }
    public string PlayerName
    {
        get; set;
    }

    public int Life
    {
        get
        {
            return life;
        }

        set
        {
            life = value;
            if (value <= 0)
            {
                KillPlayer();
            }
        }
    }

    //Virtual properties for input
    private float moveX
    {
        get { return Input.GetAxisRaw("HorizontalMove" + playerName); }
    }
    private float moveY
    {
        get { return Input.GetAxisRaw("VerticalMove" + playerName); }
    }
    private float aimX
    {
        get { return Input.GetAxisRaw("HorizontalAim" + playerName); }
    }
    private float aimY
    {
        get { return Input.GetAxisRaw("VerticalAim" + playerName); }
    }
    private float fireShot
    {
        get { return Input.GetAxisRaw("FireGrenade" + playerName); }
    }
    private float fireGrenade
    {
        get { return Input.GetAxisRaw("FireShot" + playerName); }
    }


    // Use this for initialization
    void Start()
    {
        Babies = new List<FollowerBaby>();
        personalBase = GameObject.Find("Base" + playerName).GetComponent<Base>();
        if (!personalBase.name.Contains("Base")) Debug.Log(playerName + ". Couldn't find this Player's personal base");
        playersRigidbody = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        //if the button is down(==1) call the function FireShot()
        if (!hasFiredShot && fireShot == 1)
        {
            hasFiredShot = true;
            FireShot();
        } else if (fireShot == 0)
        {
            hasFiredShot = false;
        }
        //if the grenade button is pushed and the cooldown is neutral
        if (!hasFiredGrenade && fireGrenade == 1)
        {
            hasFiredGrenade = true;
            FireGrenade();
        }
        else if (fireGrenade == 0)
        {
            hasFiredGrenade = false;
        }


        // Debug.Log(fireShot);
    }

    public void ConvertBaby(GameObject preBaby)
    {
        Debug.Log("Converted Baby");
        FollowerBaby FollowBaby = Instantiate(BabyFollowerPrefab);

        Debug.Log(playerName + " hit the baby");
        FollowBaby.GetComponent<Renderer>().material.color = playerColor;

        Babies.Add(FollowBaby);
        Babies[Babies.Count - 1].transform.position = preBaby.transform.position;
        if (Babies.Count == 1)
        {
            FollowBaby.targetTrans = this.transform;
        }
        else
        { //max following
            FollowBaby.targetTrans = Babies[Babies.Count-2].transform;
        }
        Debug.Log(Babies.Count);
        Destroy(preBaby);
    }
    public void DropBaby()
    {
        //take list of Babies, if length of list over 0, drop random baby
        if (Babies.Count > 0)
        {
            Babies.RemoveAt(Random.Range(0, Babies.Count - 1));
            Life--;
        }
    }
    private void DropAllBabies()
    {
        //called from DieFunction of this player
        //animation called here
        Babies.Clear();
    }
    private void KillPlayer()
    {
        DropAllBabies();
        //personalBase.respawn(); //not implemented yet
        //called from update when life is below 0
    }
    private void Move()
    {
        //take input, lerp between current position and target position
        transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(moveX * moveSpeed, 0f, moveY * moveSpeed), reboundTime * Time.deltaTime);
    }
    private Vector3 AimDirection()
    {
        //return direction of aim
        return new Vector3(aimX, 0f, aimY);
    }
    private void FireShot()
    {
        // Create the Bullet from the Bullet Prefab
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6.0f;
        // Add velocity to the bullet


        // Destroy the bullet after 2 seconds


    }
    private void FireGrenade()
    {
        atheistGrenadeBaby = Instantiate(
            atheistGrenadeBabyPrefab,
            grenadeSpawn.position,
            grenadeSpawn.rotation);

        // Add velocity to the baby
        atheistGrenadeBaby.GetComponent<Rigidbody>().AddForce(new Vector3(5, 5, 0), ForceMode.Impulse);

        // Destroy the bullet after 2 seconds
        Destroy(atheistGrenadeBaby, 2.0f);

    }
}
