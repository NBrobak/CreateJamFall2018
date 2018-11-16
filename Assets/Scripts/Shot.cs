using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {
    //In order for this code to work, drag the bullet prefab into the inspector. 
    //Create a empty gameobject on the player for the bulletspawn, and drag that into the inspector
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    private GameObject bullet;
    public float sec = 1f;
    // Use this for initialization
    void Start () {
        gameObject.SetActive(true);
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
            
        }


        
    }
    private void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 2.0f);

    }

   
  
}
