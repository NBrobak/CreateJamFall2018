using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    //bullet variables
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    [SerializeField]
	private float bulletCooldown = 1;
	private float bulletTimer = 0;
	[SerializeField]
	private float bulletSpeed = 10;

	//grenade Variables
	public GameObject atheistGrenadeBabyPrefab;
    public float range;
    public float speed = 3.0f;
    public float damage;
    private Animator animator;
    public Transform grenadeSpawn;
	[SerializeField]
	private float grenadeCooldown = 2;
	private float grenadeTimer = 0;
	[SerializeField]
	private float grenadeSpeed = 10;
	[SerializeField]
	private float grenadeAngledForce = 5;
    public GameObject explisionParticleEffect;
    public GameObject atheistGrenadeBaby;

    public FollowerBaby BabyFollowerPrefab;

	//Player variables
    public float moveSpeed;
	[SerializeField]
	private string playerName;
	public Color playerColor;
    private int life = 5;

    [HideInInspector]
    public List<FollowerBaby> Babies;
    private Base personalBase;
    private Rigidbody playersRigidbody;

    public int Life
    {
        get { return life; }
        set
        {
            life = value;
            if (value <= 0)
            {
                KillPlayer();
            }
        }
    }

	public string PlayerName
	{
		get
		{
			return playerName;
		}

		set
		{
			playerName = value;
		}
	}

    //Virtual properties for input
    private float moveX
    {
        get { return Input.GetAxisRaw("HorizontalMove" + PlayerName); }
    }
    private float moveY
    {
        get { return Input.GetAxisRaw("VerticalMove" + PlayerName); }
    }
    private float aimX
    {
        get { return Input.GetAxisRaw("HorizontalAim" + PlayerName); }
    }
    private float aimY
    {
        get { return Input.GetAxisRaw("VerticalAim" + PlayerName); }
    }
    private float fireShot
    {
        get { return Input.GetAxisRaw("FireGrenade" + PlayerName); }
    }
    private float fireGrenade
    {
        get { return Input.GetAxisRaw("FireShot" + PlayerName); }
    }

	// Use this for initialization
	void Start()
    {
        Babies = new List<FollowerBaby>();
        personalBase = GameObject.Find("Base" + PlayerName).GetComponent<Base>();
        if (!personalBase.name.Contains("Base")) Debug.Log(PlayerName + ". Couldn't find this Player's personal base");
		personalBase.MasterBaby = this;
        playersRigidbody = this.gameObject.GetComponent<Rigidbody>();
		bulletTimer = -bulletCooldown;
		grenadeTimer = -grenadeCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        //if the button is down(==1) call the function FireShot()
        if (fireShot > 0 && Time.timeSinceLevelLoad - bulletTimer > bulletCooldown)
        {
            FireShot();
        }
        //if the grenade button is pushed and the cooldown is neutral
        if (fireGrenade > 0 && Time.timeSinceLevelLoad - grenadeTimer > grenadeCooldown)
        {
            FireGrenade();
            StartCoroutine(Example());
        }
    }

    public void ConvertBaby(GameObject preBaby)
    {
        Debug.Log("Converted Baby");
        FollowerBaby FollowBaby = Instantiate(BabyFollowerPrefab);

        Debug.Log(PlayerName + " hit the baby");
        FollowBaby.GetComponent<Renderer>().material.color = playerColor;

        Babies.Add(FollowBaby);
        Babies[Babies.Count - 1].transform.position = preBaby.transform.position;

        FollowBaby.targetTrans = this.transform;
        if (Babies.Count > 1)
        { //max following
            Babies[Babies.Count-2].targetTrans = FollowBaby.transform;
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
        //take input, go to target position
        Vector3 newPos = transform.position + new Vector3(moveX, 0f, moveY) * moveSpeed * Time.deltaTime;
		transform.LookAt(newPos);
		transform.position = newPos;
    }
    private Vector3 AimDirection()
    {
        //return direction of aim
        return new Vector3(aimX, 0f, aimY);
    }
    private void FireShot()
    {
		bulletTimer = Time.timeSinceLevelLoad;
        // Create the Bullet from the Bullet Prefab
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
		// Add velocity to the bullet
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
		
		Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GetComponent<Collider>(), true);
    }
    private void FireGrenade()
    {
		grenadeTimer = Time.timeSinceLevelLoad;
        atheistGrenadeBaby = Instantiate(
            atheistGrenadeBabyPrefab,
            grenadeSpawn.position,
            grenadeSpawn.rotation);

        // Add velocity to the baby
        atheistGrenadeBaby.GetComponent<Rigidbody>().velocity = 
			transform.forward * grenadeSpeed + transform.up * grenadeAngledForce;

		Physics.IgnoreCollision(atheistGrenadeBaby.GetComponent<Collider>(), GetComponent<Collider>(), true);

        // Destroy the bullet after 2 seconds
        
        
        Destroy(atheistGrenadeBaby, 2.0f);
        
    }
    
    IEnumerator Example()
    {
        
        yield return new WaitForSeconds(1.8f);
        Instantiate(explisionParticleEffect, atheistGrenadeBaby.transform.position,atheistGrenadeBabyPrefab.transform.rotation);
        
    }


}
