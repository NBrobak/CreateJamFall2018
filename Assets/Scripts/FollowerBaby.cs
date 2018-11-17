using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerBaby : MonoBehaviour
{
    Transform player;
    public bool isBased = false;
    public Vector3 targetPos;
	public float moveSpeed = 5f;
    public Transform Player
    {
        get; set;
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isBased)
        {
            if (player)
            {
                moveTowardsPlayer();
            }
        }
        else
        {
            moveTowardsTarget();
        }
    }
    void moveTowardsPlayer()
    {
		Debug.Log("Moving towards player");
		Vector3.MoveTowards(transform.position, Player.transform.position, moveSpeed);
    }
    void moveTowardsTarget()
    {

    }
}
