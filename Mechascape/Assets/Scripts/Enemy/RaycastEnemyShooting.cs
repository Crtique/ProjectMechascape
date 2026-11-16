using System.Collections;
using UnityEngine;

public class RaycastEnemyShooting : MonoBehaviour
{
    [Header("Refrances")]
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Transform playerTarget;
    [SerializeField] private LineRenderer lr;
    private HealthSystem health;


    [Header("Shooting Settings")]
    [SerializeField] private float range;
    [SerializeField] private float fireRate;
    [SerializeField] private int damage;
    [SerializeField] private LayerMask hitMask;
    [SerializeField] private float chargeTime = 0.25f;
    private bool isCharging;

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
                if ( Time.time > nextFireTime && !isCharging)
                {
                    StartCoroutine(Shoot(direction));
                }
            }
        }
    }


    // Shooting the Ray
    IEnumerator Shoot(Vector2 direction)
    {
        isCharging = true;
        lr.enabled = true;
        lr.startWidth = lr.endWidth = 0.15f;
        float timer = 0f;

        //Handles charge delay
        while (timer < chargeTime)
        {
            lr.SetPosition(0, shootPoint.position);
            lr.SetPosition(1, (Vector2)shootPoint.position + direction * range);
            timer += Time.deltaTime;
            yield return null;
        }

        RaycastHit2D hit = Physics2D.Raycast(shootPoint.position, direction, range, hitMask);

        Vector2 endPoint = hit.collider != null ? hit.point : (Vector2)shootPoint.position + direction * range;

        DealDamage(hit);

        lr.startWidth = lr.endWidth = 0.4f;

        // apply visuals
        if (lr != null)
        {
            StartCoroutine(RenderLaserTracer(hit.point));
        }

        nextFireTime = Time.time + fireRate;
        isCharging = false;
    }

    //Deals damage to the player if they are hit by the Ray Cast
    void DealDamage(RaycastHit2D hit)
    {
        if (hit.collider == null) return;

        HealthSystem healthS = hit.collider.GetComponentInParent<HealthSystem>();

        if (healthS == null) return;

        healthS.TakeDamage(damage);
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
