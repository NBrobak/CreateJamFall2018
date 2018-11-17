using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {
    public GameObject atheistGrenadeBabyPrefab;
    private GameObject atheistGrenadeBaby;
    public string grenadeButton;
    public float timer;
    public float range;
    public float speed;
    public float damage;
    private Animator animator;
    public Transform grenadeSpawn;

	// Use this for initialization
	void Start () {
        animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown(grenadeButton))
        {
            ThrowBaby();

        }
    }
    private void Explode()
    {

    }
    private void ThrowBaby()
    {
        atheistGrenadeBaby = (GameObject)Instantiate(
            atheistGrenadeBabyPrefab,
            grenadeSpawn.position,
            grenadeSpawn.rotation);

        // Add velocity to the baby
        atheistGrenadeBaby.GetComponent<Rigidbody>().AddForce(new Vector3(5, 5, 0), ForceMode.Impulse);

        // Destroy the bullet after 2 seconds
        Destroy(atheistGrenadeBaby, 2.0f);

    }
}
