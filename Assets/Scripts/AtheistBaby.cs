using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtheistBaby : MonoBehaviour
{

    private bool isEnteredCollision = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();

        if (player)
        {
            Debug.Log("Entered Collision");
            if (!isEnteredCollision)
            {
                player.ConvertBaby(this.gameObject);
                isEnteredCollision = true;
            }

        }
    }
}
