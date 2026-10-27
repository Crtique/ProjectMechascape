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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check if when the last time a bullet has been shot
        if (Time.time - timeOfLastShot >= bulletDelay)
        {
            Shoot();
            timeOfLastShot = Time.time;
        }
    }

    void Shoot()
    {
        tr.emitting = true;

        // Shooting function
        GameObject bulletInst = Instantiate(bullet, shootPoint.position, shootPoint.rotation);

        bulletInst.GetComponent<Rigidbody2D>().AddForce(bulletInst.transform.right * bulletSpeed);
        Destroy(bulletInst, 3);
    }
}
