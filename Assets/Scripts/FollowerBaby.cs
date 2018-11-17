using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerBaby : MonoBehaviour
{
    Transform player;
    public Transform Player
    {
        get; set;
    }
    public bool isbased;
	public Vector3 targetPos;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            moveTowardsPlayer();
        }
    }
    void moveTowardsPlayer()
    {

    }
}
