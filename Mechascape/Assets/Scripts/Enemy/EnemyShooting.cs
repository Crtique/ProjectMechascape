using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [Header("Weapon Shooting Settings")]
    [SerializeField] private Transform shootPoint;
    

    [Header("Bullet Fire Settings")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private float bulletDelay = 0.5f;
    [SerializeField] private float timeOfLastShot;

    [Header("Player Detection")]
    [SerializeField] private Transform playerTarget;
    [SerializeField] private float range;
    bool playerInRange;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Auto Assign the player
        GameObject playerObj = GameObject.FindWithTag("Player");

        if (playerObj != null)
        {
            playerTarget = playerObj.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayerDistance();
        if (playerInRange)
        {
            // Check if when the last time a bullet has been shot
            if (Time.time - timeOfLastShot >= bulletDelay)
            {
                Shoot();
                timeOfLastShot = Time.time;
            }
        }
    }

    void Shoot()
    {
        tr.emitting = true;
        Vector2 shootDirection = (playerTarget.position - shootPoint.position).normalized;

        // Calculate the matching world rotation for the bullet sprite
        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        Quaternion bulletRotation = Quaternion.Euler(0f, 0f, angle);

        // Shooting function
        GameObject bulletInst = Instantiate(bullet, shootPoint.position, bulletRotation);

        bulletInst.GetComponent<Rigidbody2D>().AddForce(shootDirection * bulletSpeed);
        Destroy(bulletInst, 3);
    }

    void DetectPlayerDistance()
    {
        float distance = Vector2.Distance(transform.position, playerTarget.position);

        playerInRange = distance <= range;
    }
}
