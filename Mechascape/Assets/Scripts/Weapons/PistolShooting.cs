using UnityEngine;

public class PistolShooting : MonoBehaviour
{
    [SerializeField] private WeaponKnockback wkb;
    [SerializeField] private Transform gun;

    [SerializeField] private Transform shootPoint;

    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeed;
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
        // Shooting function
        GameObject bulletInst = Instantiate(bullet, shootPoint.position, shootPoint.rotation);

        bulletInst.GetComponent<Rigidbody2D>().AddForce(bulletInst.transform.right * bulletSpeed);

        wkb.ApplyKnockbackForce();

    }
}
