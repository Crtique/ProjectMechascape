using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spikes : MonoBehaviour
{
	public int damage = 10;
	[SerializeField] HealthSystem healthSystem;

    private void Awake()
    {
        healthSystem = FindFirstObjectByType<HealthSystem>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
	{
        healthSystem.TakeDamage(damage);
    }
}
