using UnityEngine;

public class PistolShooting : MonoBehaviour
{
    [SerializeField] private WeaponKnockback wkb;

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

        wkb.ApplyKnockbackForce();

    }
}
