using UnityEngine;

/// <summary>
/// This script was made by CJ Robinson. This script handles all of the Collision Detection 
/// regarding taking damage or picking up item drops such as: Health, or Ammo. This script also 
/// deals damage to the player when it get hit by the gunner enemy.
/// If you make any changes please mark them here:
/// 
/// </summary>

public class CollisionDetector : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PistolShooting ps;         // Pistol Shooting script
    [SerializeField] private HealthSystem hs;           // Player Health System script
    [SerializeField] private EnemyHealthSystem ehs;     // Enemy Health System script

    private readonly int ammo = 10; // Number that the ammunition provides
    private readonly int heal = 10;

    private readonly int damage = 10;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Pick Up Ammo
        if (gameObject.CompareTag("Player") && collision.CompareTag("AmmoDrop"))
        {
            ps.UpdateAmmoReserve(ammo);
            Destroy(collision.gameObject);
        }

        // Pick up Health
        else if (gameObject.CompareTag("Player") && collision.CompareTag("HealthDrop"))
        {
            hs.Heal(heal);
            Destroy(collision.gameObject);
        }

        // Deal damage to player when the gunner shoots them
        else if (gameObject.CompareTag("Player") && collision.CompareTag("Projectile"))
        {
            hs.TakeDamage(damage);
            Destroy(collision.gameObject);
        }

        #region ENEMY DAMAGE

        // Deal Damage to the Gunner enemy
        else if (gameObject.CompareTag("RangedEnemy") && collision.CompareTag("Bullet") || collision.CompareTag("KickCollider"))
        {
            ehs.EnemyTakeDamage(damage);
            Destroy(collision.gameObject);
            Debug.Log("RangedEnemy Took Damage!");
        }

        // Deal Damage to the Turret Dog Enemy
        else if (gameObject.CompareTag("TurrentEnemy") && collision.CompareTag("Bullet") || collision.CompareTag("KickCollider"))
        {
            ehs.EnemyTakeDamage(damage);
            Destroy(collision.gameObject);
            Debug.Log("Turret Took Damage!");
        }

        // Deals Damage to the Claw enemy
        else if (gameObject.CompareTag("MeleeEnemy") && collision.CompareTag("Bullet") || collision.CompareTag("KickCollider"))
        {
            ehs.EnemyTakeDamage(damage);
            Destroy(collision.gameObject);
            Debug.Log("MeleeEnemy Took Damage!");
        }
        #endregion
    }
}
