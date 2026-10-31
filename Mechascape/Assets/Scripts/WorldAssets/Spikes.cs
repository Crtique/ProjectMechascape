using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spikes : MonoBehaviour
{
	public float damage = 10.0f;
	private void OnCollisionEnter2D(Collision2D collision)
	{
		HealthSystem health = collision.collider.GetComponent<HealthSystem>();
		if(health != null)
		{
			health.TakeDamage(damage);
		}
	}
}
