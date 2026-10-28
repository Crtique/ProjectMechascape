using UnityEngine;

public class ParryHitBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Controls how the projectile or the general Hitbox will be programmed for the projectile to move
        if (collision.CompareTag("Projectile"))
        {
            Rigidbody2D projectileRb = collision.GetComponent<Rigidbody2D>();

            if (projectileRb != null)
            {
                projectileRb.linearVelocity = -projectileRb.linearVelocity;

                Debug.Log("Parry Success");
            }
        }
    }
}
