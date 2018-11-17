using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FollowerBaby : MonoBehaviour
{

    public bool isBased = false;
    public float moveSpeed = 5f;
    public float displacement = 5f;
    public Vector3 targetPos;
	public Action onArrivalAtBase;

    public Transform TargetTrans
    {
        get; set;
    }

    void Update()
    {
        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        if (isBased)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
			if(transform.position == targetPos)
			{
				onArrivalAtBase();
			}
        }
        else
        {
			Vector3 temp = TargetTrans.position - (TargetTrans.position - transform.position).normalized * displacement;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(temp.x, transform.position.y, temp.z), moveSpeed * Time.deltaTime);
        }
    }

	public void MoveToBase(Vector3 positionInBase, bool stay)
	{
		isBased = true;
		targetPos = positionInBase;
		if (stay)
		{
			onArrivalAtBase += () => { enabled = false; Destroy(GetComponent<Rigidbody>()); };
		}
		else
		{
			onArrivalAtBase += () => Destroy(gameObject);
		}
	}
}
