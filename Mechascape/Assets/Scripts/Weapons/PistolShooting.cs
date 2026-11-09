using UnityEngine;

public class PistolShooting : MonoBehaviour
{
    [Header("Weapon Knockback Refrance")]
    [SerializeField] private WeaponKnockback wkb;

    [Header("Weapon Shooting Settings")]
    [SerializeField] private Transform gun;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private int maxAmmo = 10;
    [SerializeField] private int currentAmmo;

    [Header("Bullet Fire Settings")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private float bulletDelay = 2f;
    [SerializeField] private float LastBulletShot;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time - LastBulletShot >= bulletDelay && currentAmmo > 0)
        {
            Shoot();
            currentAmmo -= 1;
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
    
    public void UpdateAmmo(int addAmmo)
    {
        if (currentAmmo < maxAmmo)
        {
            currentAmmo += addAmmo;
            if (currentAmmo >  maxAmmo)
            {
                maxAmmo = currentAmmo;
            }
        }

    }
}
