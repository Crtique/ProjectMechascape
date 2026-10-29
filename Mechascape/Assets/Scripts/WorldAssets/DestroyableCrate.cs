using UnityEngine;

public class DestroyableCrate : MonoBehaviour
{
    [SerializeField] private PlayerController pc;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && pc.isGroundSlamming)
        {
            pc.GroundSlammingDamage(10);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
