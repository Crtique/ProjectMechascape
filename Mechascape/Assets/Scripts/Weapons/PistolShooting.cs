using UnityEngine;

public class PistolShooting : MonoBehaviour
{
    [Header("Weapon Knockback Reference")]
    [SerializeField] private WeaponKnockback wkb;

    [Header("Weapon Shooting Settings")]
    [SerializeField] private Transform gun;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform groundCheck;

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

    private bool IsGrounded()
    {
        float radius = 0.2f;

        return Physics2D.OverlapCircle(groundCheck.position, radius, groundMask);
    }

    void Shoot()
    {
        tr.emitting = true;

        // Shooting function
        GameObject bulletInst = Instantiate(bullet, shootPoint.position, shootPoint.rotation);

        bulletInst.GetComponent<Rigidbody2D>().AddForce(bulletInst.transform.right * bulletSpeed);
        Destroy(bulletInst, 3);

        if(!IsGrounded())
        {
            wkb.ApplyKnockbackForce();
        }
    }
}
