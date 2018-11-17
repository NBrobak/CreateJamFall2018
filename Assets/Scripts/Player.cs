using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    //bullet variables
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    private GameObject bullet;
    private float bulletSpeed = 6.0f;


    //grenade Variables
    public GameObject atheistGrenadeBabyPrefab;
    private GameObject atheistGrenadeBaby;
    public float timer;
    public float range;
    public float speed=3.0f;
    public float damage;
    private Animator animator;
    public Transform grenadeSpawn;

    public GameObject BabyFollowerPrefab;
    public float moveSpeed;
    public float reboundTime = 0.3f;
    private float moveX;
    private float moveY;
    private float aimX;
    private float aimY;
    private float fireShot;
    private float fireGrenade;
    public string playerName;
    private int life = 5;

    private List<FollowerBaby> Babies;
    private float cooldown;
    private Base personalBase;
    private Rigidbody playersRigidbody;


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
        SetInputs();    //sets controller inputs based on PlayerName
        if (life <= 0)
        {
            KillPlayer();
        }
        Move();

        //if the button is down(==1) call the function FireShot()
        if (fireShot == 1)
        {
            FireShot();
        }
        //if the grenade button is pushed and the cooldown is neutral
        if (fireGrenade == 1 && timer == 0)
        {
            FireGrenade();
            //being cooldown timer
        }


    }

    private void SetInputs()
    {
        moveX = Input.GetAxisRaw("HorizontalMove" + playerName);
        moveY = Input.GetAxisRaw("VerticalMove" + playerName);
        aimX = Input.GetAxisRaw("HorizontalAim" + playerName);
        aimY = Input.GetAxisRaw("VerticalAim" + playerName);
        fireGrenade = Input.GetAxisRaw("FireGrenade" + playerName);
        fireShot = Input.GetAxisRaw("FireShot" + playerName);
    }
    void FixedUpdate()
    {
        
    }
    private void ConvertBaby(AtheistBaby preBaby)
    {
        Babies.Add(Instantiate(BabyFollowerPrefab).GetComponent<FollowerBaby>());
        Babies[Babies.Count - 1].transform.position = preBaby.transform.position;
        Destroy(preBaby.gameObject);
    }
    public void DropBaby()
    {
        //take list of Babies, if length of list over 0, drop random baby
        if (Babies.Count > 0)
        {
            Babies.RemoveAt(Random.Range(0, Babies.Count - 1));
            life--;
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
        bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 2.0f);
        
    }
    private void FireGrenade()
    {
        atheistGrenadeBaby = (GameObject)Instantiate(
            atheistGrenadeBabyPrefab,
            grenadeSpawn.position,
            grenadeSpawn.rotation);

        // Add velocity to the baby
        atheistGrenadeBaby.GetComponent<Rigidbody>().AddForce(new Vector3(5, 5, 0), ForceMode.Impulse);

        // Destroy the bullet after 2 seconds
        Destroy(atheistGrenadeBaby, 2.0f);
        
    }
}
