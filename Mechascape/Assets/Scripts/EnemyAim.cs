using UnityEngine;

public class EnemyAim : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform player;

    [Header("Enemy")]
    [SerializeField] private Transform enemySprite;
    [SerializeField] private Transform gun;

    [SerializeField] private float flipDeadzone = 0.15f;

    bool facingLeft;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;
        AimTowardsPlayer();
    }

    void AimTowardsPlayer()
    {
        Vector2 direction = player.position - transform.position;
        if (Mathf.Abs(direction.x) > flipDeadzone)
        {
            facingLeft = direction.x < 0f;
            enemySprite.localScale = new Vector3(facingLeft ? -1f : 1f, 1f, 1f);
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (facingLeft)
        {
            // Accounts for EnemyPivotPoint's -1 X scale
            gun.localRotation = Quaternion.Euler(0f, 0f, 180f - angle);
        }
        else
        {
            gun.localRotation = Quaternion.Euler(0f, 0f, angle);
        }

        // Keep gun's local scale at (1, 1, 1) — the parent flip already mirrors it horizontally
        gun.localScale = Vector3.one;
    }
}
