using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
	public GameObject BabyFollowerPrefab;
	public float moveSpeed;
    private float moveX;
    private float moveY;
    private float aimX;
    private float aimY;
    private float fireShot;
    private float fireGrenade;
    private string playerName;
    private int life = 5;

    private List<FollowerBaby> Babies;
    private float cooldown;
    private Base personalBase;

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
    }

    // Update is called once per frame
    void Update()
    {
        SetInputs();    //sets controller inputs based on PlayerName
		if(life <= 0){
			KillPlayer();
		}
    }

    private void SetInputs()
    {
        moveX = Input.GetAxis("HorizontalMove" + playerName);
        moveY = Input.GetAxis("VerticalMove" + playerName);
        aimX = Input.GetAxis("HorizontalAim" + playerName);
        aimY = Input.GetAxis("VerticalAim" + playerName);
        fireGrenade = Input.GetAxis("FireGrenade" + playerName);
        fireShot = Input.GetAxis("FireShot" + playerName);
    }
    private void ConvertBaby(AtheistBaby preBaby)
    {
		Babies.Add(Instantiate(BabyFollowerPrefab).GetComponent<FollowerBaby>());
		Babies[Babies.Count-1].transform.position = preBaby.transform.position;
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
        transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(moveX, 0f, moveY), moveSpeed * Time.deltaTime);
    }
    private Vector3 AimDirection()
    {
        //return direction of aim
        return new Vector3(aimX, 0f, aimY);
    }
    private void FireShot()
    {

        //creates a shot object with direction
    }
    private void FireGrenade()
    {

        //creates a grenade object with calculated target
    }
}
