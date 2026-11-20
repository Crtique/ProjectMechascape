using UnityEngine;

public class EnemyAim : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform player;

    [Header("Enemy")]
    [SerializeField] private Transform enemySprite;
    [SerializeField] private Transform gun;

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

        bool faceingLeft = direction.x < 0;

        // Flip the sprite
        enemySprite.localScale = new Vector3(faceingLeft ? -1 : 1, 1, 1);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the gun
        gun.rotation = Quaternion.Euler(0f, 0f, angle);

        // Flip the gun vertically when aiming left
        Vector3 scale = gun.localScale;
        scale.y = direction.x < 0 ? -1 : 1;
        gun.localScale = scale;
    }
}
