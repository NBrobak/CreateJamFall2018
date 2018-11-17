using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerBaby : MonoBehaviour
{

    public bool isBased = false;
    // public Vector3 targetPos;
    public float moveSpeed = 5f;
    public float displacement = 5f;
    public Transform targetTrans;
    public Vector3 targetPos;

    public Transform TargetTrans
    {
        get
        {
            return this.targetTrans;
        }
        set
        {
            this.targetTrans = value;
        }
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
        if (isBased)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
        }
        else
        {
			Vector3 temp = targetTrans.position - (targetTrans.position - transform.position).normalized * displacement;
            transform.position = Vector3.Lerp(transform.position, new Vector3(temp.x, 0, temp.z), moveSpeed * Time.deltaTime);
        }

    }
}
