using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PistolShooting ps;
    [SerializeField] private HealthSystem hs;

    private int ammo = 10; // Number that the ammunition provides
    private int heal = 10;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("Player") && collision.CompareTag("AmmoDrop"))
        {
            ps.UpdateAmmoReserve(ammo);
            Destroy(collision.gameObject);
        }

        else if (gameObject.CompareTag("Player") && collision.CompareTag("HealthDrop"))
        {
            hs.Heal(heal);
            Destroy(collision.gameObject);
        }
    }
}
