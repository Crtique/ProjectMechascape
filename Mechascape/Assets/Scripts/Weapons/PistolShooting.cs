using UnityEngine;
using UnityEngine.UI;

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

    public Slider ammoSlider;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmo();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time - LastBulletShot >= bulletDelay && currentAmmo > 0)
        {
            Shoot();
            currentAmmo -= 1;
            UpdateAmmo();
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
        currentAmmo = Mathf.Clamp(currentAmmo + addAmmo, 0, maxAmmo);

        UpdateAmmo();

    }
    void UpdateAmmo()
    {
        if(ammoSlider != null)
        {
            ammoSlider.value = currentAmmo;
        }
    }
}
