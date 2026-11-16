using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class PistolShooting : MonoBehaviour
{
    [Header("Weapon Knockback Refrance")]
    [SerializeField] private WeaponKnockback wkb;

    [Header("Weapon Shooting Settings")]
    [SerializeField] private Transform gun;
    [SerializeField] private Transform shootPoint;

    [Header("Ammunition Settings")]
    [SerializeField] private int maxAmmo = 10;
    [SerializeField] private int currentAmmo;
    [SerializeField] private int ammoReserve = 25;
    [SerializeField] private int ammoMaxReserve = 50;

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
        UpdateAmmoUI();
    }

    // Update is called once per frame
    void Update()
    {
        WeaponInputs();
    }

    // Inputs for our weapon
    void WeaponInputs()
    {
        // Shoot
        if (Input.GetMouseButtonDown(0) && Time.time - LastBulletShot >= bulletDelay && currentAmmo > 0)
        {
            Shoot();
            currentAmmo -= 1;
            UpdateAmmoUI();
        }

        // Reload
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    // Shoot our gun
    void Shoot()
    {
        tr.emitting = true;

        // Shooting function
        GameObject bulletInst = Instantiate(bullet, shootPoint.position, shootPoint.rotation);

        bulletInst.GetComponent<Rigidbody2D>().AddForce(bulletInst.transform.right * bulletSpeed);
        Destroy(bulletInst, 3);

        wkb.ApplyKnockbackForce();

    }
    
    // Method to Reload our gun
    void Reload()
    {
        if (currentAmmo < maxAmmo && ammoReserve > 0)
        {
            // Calculate how much ammo we need
            int bulletsNeed = maxAmmo - currentAmmo;

            // Check how many we can actually Move over from our reserve
            int transferBullets = Mathf.Min(bulletsNeed, ammoReserve);

            // Actually Move the Ammo over to our currentAmmo
            currentAmmo += transferBullets;
            ammoReserve -= transferBullets;
        }
    }

    // Add ammo to the players reserve
    // We need a UI asset for the ammo reserve
    public void UpdateAmmoReserve(int addAmmo)
    {
        ammoReserve = Mathf.Clamp(ammoReserve + addAmmo, 0, ammoMaxReserve);

    }

    // Update ammo UI (this is just a method to track how much we have visually)
    void UpdateAmmoUI()
    {
        if(ammoSlider != null)
        {
            ammoSlider.value = currentAmmo;
        }
    }
}
