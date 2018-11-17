using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerBaby : MonoBehaviour
{

    public bool isBased = false;
    public Vector3 targetPos;
    public float moveSpeed = 5f;
    public Player player
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
            moveTowardsPlayer();
        }
        else
        {
            moveTowardsTarget();
        }
    }
    void moveTowardsPlayer()
    {
        Debug.Log("Moving towards player");
		Debug.Log(player);
		Vector3 targetPos = player.
        transform.position = Vector3.Lerp(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }
    void moveTowardsTarget()
    {

    }
}
