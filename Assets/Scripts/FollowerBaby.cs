using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerBaby : MonoBehaviour
{

    public bool isBased = false;
    // public Vector3 targetPos;
    public float moveSpeed = 5f;
    public float displacement = 5f;
    public Transform targetPos
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
        moveTowardsTarget();
    }
    // void moveTowardsPlayer()
    // {
    //     Debug.Log("Moving towards player");
    //     //Debug.Log(player);
    //     Vector3 targetPos = (player.transform.position - transform.position) - transform.forward * displacement;
    //     transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
    // }
    void moveTowardsTarget()
    {
		//Vector3 ongoingtargetPos = (player.transform.position - transform.position) - transform.forward * displacement;
        transform.position = Vector3.Lerp(transform.position, targetPos.position, moveSpeed * Time.deltaTime);
    }
}
