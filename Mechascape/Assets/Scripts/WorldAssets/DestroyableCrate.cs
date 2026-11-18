using UnityEngine;

public class DestroyableCrate : MonoBehaviour
{
    [Header("Item Drops")]
    [SerializeField] private GameObject[] pickUpDrops;
    private readonly float dropChance = 0.7f;

    private PlayerController pc;

    private void Start()
    {
        pc = FindFirstObjectByType<PlayerController>();

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && pc.isGroundSlamming)
        {
            pc.GroundSlammingDamage(10);
            DestroyCrate();
        }

        else if (collision.gameObject.CompareTag("Bullet"))
        {
            //DestroyCrate();
            DestroyCrate();
            Destroy(collision.gameObject);
        }
    }

    void DestroyCrate()
    {
        Destroy(this.gameObject);
        DropPickUps();
    }

    void DropPickUps()
    {
        if (pickUpDrops.Length == 0) return;

        if (Random.value <= dropChance && pickUpDrops.Length > 0)
        {
            int index = Random.Range(0, pickUpDrops.Length);
            Instantiate(pickUpDrops[index], transform.position, Quaternion.identity);
        }
    }
}
