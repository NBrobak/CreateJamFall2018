using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerBaby : MonoBehaviour
{
    Transform player;
    public bool isBased;
    public Vector3 targetPos;
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

    }
    void moveTowardsTarget()
    {

    }
}
