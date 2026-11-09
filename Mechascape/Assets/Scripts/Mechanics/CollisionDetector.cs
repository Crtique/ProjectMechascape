using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PistolShooting ps;

    private int ammo = 10; // Number that the ammunition provides


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("Player") && collision.CompareTag("AmmoDrop"))
        {
            ps.UpdateAmmo(ammo);
            Destroy(collision.gameObject);
        }
    }
}
