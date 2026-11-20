using UnityEngine;

public class EnemyAim : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform player;

    [Header("Enemy")]
    [SerializeField] private Transform enemySprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        

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

        bool faceingLeft = direction.x < 0;

        // Flip the sprite
        enemySprite.localScale = new Vector3(faceingLeft ? -1 : 1, 1, 1);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
