using Unity.VisualScripting;
using UnityEngine;

public class BreakableWorldObjects : MonoBehaviour
{
    [SerializeField] private PlayerController pc;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pc = FindFirstObjectByType<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && pc.isGroundSlamming)
        {
            pc.GroundSlammingDamage(10);
            Destroy(this.gameObject);
        }
    }
}