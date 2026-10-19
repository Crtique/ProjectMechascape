using UnityEngine;

public class PistolShooting : MonoBehaviour
{
    [Header("Weapon Knockback Refrance")]
    [SerializeField] private WeaponKnockback wkb;

    [Header("Weapon Shooting Settings")]
    [SerializeField] private Transform gun;
    [SerializeField] private Transform shootPoint;

    [Header("Bullet Fire Settings")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private TrailRenderer tr;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        tr.emitting = true;

        // Shooting function
        GameObject bulletInst = Instantiate(bullet, shootPoint.position, shootPoint.rotation);

        bulletInst.GetComponent<Rigidbody2D>().AddForce(bulletInst.transform.right * bulletSpeed);
        Destroy(bulletInst, 3);

        wkb.ApplyKnockbackForce();

    }
}
