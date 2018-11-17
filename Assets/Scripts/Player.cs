using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public Animator animator;
    private float moveX;
    private float moveY;
    private float aimX;
    private float aimY;
    private float fireShot;
    private float fireGrenade;
    private float playerName;
    private float life;
    private float moveSpeed;
    private List<FollowerBaby> Babies;
    private float cooldown;

    private float Cooldown
    {
        get
        {
            return this.cooldown;
        }
        set
        {
			if(value > 0f){
            	this.cooldown = value;
			}
        }
    }




    // Use this for initialization
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
        SetInputs();


		


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
	private void ConvertBaby(AtheistBaby preBaby){
		//take baby, delete preBaby, but create FollowerBaby at same locations
		Instantiate(new FollowerBaby());
	}
	public void DropBaby(){
		//take list of Babies, if length of list over 0, drop random baby
	}
	private void DropAllBabies(){
		//called from DieFunction of this player
	}
	private void KillPlayer(){
		//called from update when life is below 0
	}
	private void Move(){
		//take input, lerp between current position and target position
	}
	private Vector3 AimDirection(){
		//return direction of aim
		return new Vector3(0f,0f,0f);
	}
	private void FireShot(){
		//creates a shot object with direction
	}
	private void FireGrenade(){
		//creates a grenade object with calculated target
	}
}
