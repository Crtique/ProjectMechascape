using UnityEngine;

public class WeaponKnockback : MonoBehaviour
{
    [Header("Refrances")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Knockback Settings")]
    [SerializeField] private float knockbackForce;
    [SerializeField] private float knockbackDuration;

    private float knockbackTimer;

    // Check our timer is Zero (False) or not Zero (True)
    // This is a shorter way of writing properties and returns a value thats either Ture or False
    public bool IsKnockedBack => knockbackTimer > 0f;

    private void FixedUpdate()
    {
        // Check if out timer is greater than zero then subtract that time by the time of the game
        if (knockbackTimer > 0f)
        {
            knockbackTimer -= Time.deltaTime;
        }
    }

    // ---- KNOCKBACK FUNCTION ---- //
    public void ApplyKnockbackForce()
    {
        // Get the direction that the weapon is aiming
        Vector2 shootDirection = transform.right;

        // Knock the actual Player backwards/recoil
        Vector2 knockBackDirection = -shootDirection.normalized;

        rb.AddForce(knockBackDirection * knockbackForce, ForceMode2D.Impulse);

        knockbackTimer = knockbackDuration;
    }
}
