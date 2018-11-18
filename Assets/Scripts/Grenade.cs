using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Grenade : MonoBehaviour {
	
    public float timer = 2;
    public float explosionRadius = 2;
    public int damage = 2;

	// Use this for initialization
	void Start () 
    {
        StartCoroutine(Explode());
	}

    private IEnumerator Explode()
    {
		yield return new WaitForSeconds(timer);
        IEnumerable<Player> nearbyPlayers = 
			Physics.OverlapSphere(transform.position, explosionRadius)
				.Where(c => c.CompareTag("Player"))
				.Select(c => c.GetComponent<Player>());
		foreach (Player player in nearbyPlayers)
		{
			player.TakeDamage(damage);
		}

		Destroy(gameObject);
    }
}
