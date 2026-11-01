using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Explosive : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float force;
    public float damage = 10.0f;
    [SerializeField] private PlayerController pc;

    public void Explosion()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach(Collider2D collider in objects)
        {
            Rigidbody2D rb2D = collider.GetComponent<Rigidbody2D>();
            if(rb2D != null)
            {
                //Force Calculation
                Vector2 direction = collider.transform.position - transform.position;
                float distance = 1 + direction.magnitude;
                float forceFinal = force / distance;
                rb2D.AddForce(direction * forceFinal);

                //Damage Calculation
                HealthSystem health = collider.GetComponent<HealthSystem>();
		        if(health != null)
		        {
			        health.TakeDamage(damage);
		        }
            }
        }
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && pc.isGroundSlamming)
        {
            Explosion();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Explosion();
        }
    }
}
