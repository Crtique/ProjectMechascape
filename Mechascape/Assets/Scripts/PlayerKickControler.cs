using UnityEngine;

public class PlayerKickControler : MonoBehaviour
{
    [SerializeField] private Collider2D kickCollider;
    private readonly float kickCooldown = 1f;
    private float nextKickTime;
    int damage = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        kickCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextKickTime && Input.GetKeyDown(KeyCode.F))
        {
            kickCollider.enabled = true;
            Debug.Log("Kick! " + damage + " damage dealt");

            nextKickTime = Time.time + kickCooldown;
        }
    }
}
