using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {
  
	[SerializeField]
	private int damage = 1;

	private void OnTriggerEnter(Collider collider)
	{
		Destroy(gameObject);
		if (collider.CompareTag("Player"))
		{
			Player player = collider.GetComponent<Player>();
			player.TakeDamage(damage);
		}
	}
}
