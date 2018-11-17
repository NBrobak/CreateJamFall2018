using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {
    //In order for this code to work, drag the bullet prefab into the inspector. 
    //Create a empty gameobject on the player for the bulletspawn, and drag that into the inspector
    public string shootButton;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    private GameObject bullet;
    private float bulletSpeed = 6.0f;
    
    // Use this for initialization
    void Start () {
        
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(shootButton))
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
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 2.0f);

    }

   
  
}
