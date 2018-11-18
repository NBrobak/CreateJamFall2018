using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
	public static readonly int MAX_HEALTH = 3;

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
    private GameObject atheistGrenadeBaby;
    public GameObject pentagram; 
    public FollowerBaby BabyFollowerPrefab;
    private bool dontSpamCube=false;
    private bool dontSpamEx = false;
    private bool dontSpamPent = false;
    public GameObject deSpawnSmoke;
	[SerializeField]
	private Renderer healtIcon;

	//Player variables
    public float moveSpeed;
	[SerializeField]
	private string playerName;
	public Color playerColor;
    public Material[] colorMats;
    public Renderer[] colorRenders;
    private int life;

    public List<FollowerBaby> Babies;
    private Base personalBase;
    private Rigidbody playersRigidbody;

    public int Life
    {
        get { return life; }
        set
        {
            life = value;
			float healthPercent = (float)(value - 1) / (MAX_HEALTH - 1);
			healtIcon.material.color = Color.Lerp(Color.red, Color.green, healthPercent);
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
        animator = GetComponent<Animator>();
        Babies = new List<FollowerBaby>();
        personalBase = GameObject.Find("Base" + PlayerName).GetComponent<Base>();
        if (!personalBase.name.Contains("Base")) Debug.Log(PlayerName + ". Couldn't find this Player's personal base");
		personalBase.MasterBaby = this;
        playersRigidbody = this.gameObject.GetComponent<Rigidbody>();
		bulletTimer = -bulletCooldown;
		grenadeTimer = -grenadeCooldown;
		Life = MAX_HEALTH;


        for (int i = 0; i < colorMats.Length; i++)
        {
            Material mat = new Material(colorMats[i]);
            mat.color = playerColor;
            colorRenders[i].material = mat;
        }
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
            if (Babies.Count > 0)
            {
                FireGrenade();
            }
        }

        animator.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));
    }

    public void ConvertBaby(GameObject preBaby)
    {
        FollowerBaby FollowBaby = Instantiate(BabyFollowerPrefab);

        Debug.Log(PlayerName + " hit the baby");
        if (FollowBaby)
        {
            FollowBaby.SetDiaperColor(playerColor);

            Babies.Add(FollowBaby);
            Babies[Babies.Count - 1].transform.position = preBaby.transform.position;

            FollowBaby.TargetTrans = transform;
            if (Babies.Count > 1)
            {
                Babies[Babies.Count - 2].TargetTrans = FollowBaby.transform;
            }
            Destroy(preBaby);
        }
    }
    public void DropBabies(int babiesToDrop)
    {
		for (int i = 0; i < babiesToDrop && Babies.Any(); i++)
		{
			FollowerBaby lastBaby = Babies.First();
			DropBaby(lastBaby);
		}
	}

	private void DropBaby(FollowerBaby baby)
	{
		Babies.Remove(baby);
		SpawnController.Instance.SpawnBaby(baby.transform.position);
		Destroy(baby.gameObject);
	}

	private void DropAllBabies()
    {
		foreach (FollowerBaby baby in Babies.ToList())
		{
			DropBaby(baby);
		}
    }

	public void TakeDamage(int damage)
	{
		DropBabies(damage);
		Life -= damage;
		Debug.Log(playerName + " health " + Life);
	}

    private void KillPlayer()
    {
        DropAllBabies();
        personalBase.Respawn();
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

        if(dontSpamPent == false)
        {

            dontSpamPent = true;
            StartCoroutine(SendChildGrenade());
        }

    }
    
    IEnumerator GreenBoom()
    {
        yield return new WaitForSeconds(1.8f);
        GameObject temp2 = Instantiate(explisionParticleEffect, atheistGrenadeBaby.transform.position, atheistGrenadeBabyPrefab.transform.rotation);
        Destroy(temp2, 4f) ;
    }
    IEnumerator SendChildGrenade()
    {
        FollowerBaby despawnedChild = Babies[0];
        Babies.RemoveAt(0);
        if (despawnedChild)
        {
            GameObject despawnParticleEffect = Instantiate(deSpawnSmoke, despawnedChild.transform);
            despawnParticleEffect.transform.position = new Vector3(despawnParticleEffect.transform.position.x, 0f, despawnParticleEffect.transform.position.z);
            Destroy(despawnedChild.gameObject, 1f);
        }
 
        yield return new WaitForSeconds(1f);

        grenadeTimer = Time.timeSinceLevelLoad;
        atheistGrenadeBaby = Instantiate(
            atheistGrenadeBabyPrefab,
            grenadeSpawn.position,
            grenadeSpawn.rotation);
        StartCoroutine(GreenBoom());

        // Add velocity to the baby
        atheistGrenadeBaby.GetComponent<Rigidbody>().velocity =
            transform.forward * grenadeSpeed + transform.up * grenadeAngledForce;

        Physics.IgnoreCollision(atheistGrenadeBaby.GetComponent<Collider>(), GetComponent<Collider>(), true);
        // Destroy the bullet after 2 seconds
        GameObject temp = Instantiate(pentagram, grenadeSpawn.transform);
        temp.transform.SetParent(this.gameObject.transform);
        
        Destroy(temp, 2.0f);
        
        dontSpamPent = false;
    }



}

