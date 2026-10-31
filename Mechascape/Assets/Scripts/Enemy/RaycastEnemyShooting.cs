using System.Collections;
using UnityEngine;

public class RaycastEnemyShooting : MonoBehaviour
{
    [Header("Refrances")]
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Transform playerTarget;
    [SerializeField] private LineRenderer lr;

    [Header("Shooting Settings")]
    [SerializeField] private float range;
    [SerializeField] private float fireRate;
    [SerializeField] private float damage;
    [SerializeField] private LayerMask hitMask;

    private float nextFireTime;

    // Update is called once per frame
    void Update()
    {
        DetectPlayer();
    }

    // ---- Player Detection ---- //
    void DetectPlayer()
    {
        if (playerTarget == null) return;
        
        // Calculate the direction to our player character
        Vector2 direction = (playerTarget.position - shootPoint.position).normalized;

        // Cast a ray from the shooting point to the player within its maxium range
        RaycastHit2D hit = Physics2D.Raycast(shootPoint.position, direction, range, hitMask);

        // Debug visualization so we can see what is happening
        Debug.DrawRay(shootPoint.position, direction * hit.distance, Color.green);

        if (hit.collider != null)
        {
            // We have a line of sight if our first hit is out player
            if (hit.collider.CompareTag("Player"))
            {
                Debug.DrawRay(shootPoint.position, direction * hit.distance, Color.red);

                // Check cooldown time
                if ( Time.time > nextFireTime)
                {
                    Shoot(hit, direction);
                    nextFireTime = Time.time + fireRate;
                }
            }
        }
    }


    // Shooting the Ray
    void Shoot(RaycastHit2D hit, Vector2 direction)
    {
        // Deal damage to the player if we get the collider component
        if (hit.collider.TryGetComponent<HealthSystem>(out HealthSystem healthSystem))
        {
            healthSystem.TakeDamage(damage);
        }

        // apply visuals
        if (lr != null)
        {
            StartCoroutine(RenderLaserTracer(hit.point));
        }
    }

    private IEnumerator RenderLaserTracer(Vector2 hitPoint)
    {
        lr.enabled = true;
        
        // Set LineRender Positions
        lr.SetPosition(0, shootPoint.position);
        lr.SetPosition(1, hitPoint);

        // keep the laser visible for a half second
        yield return new WaitForSeconds(0.5f);
        lr.enabled = false;
    }

}
