using System.Collections;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] public float radius;
    [Range(0, 360)]
    [SerializeField] public float angle;

    [Header("Refrances")]
    [SerializeField] private SpriteRenderer enemySprite;
    [SerializeField] private Transform enemyPivotPoint;
    [SerializeField] public GameObject playerRef;
    [SerializeField] public LayerMask targetMask;
    [SerializeField] public LayerMask obstructionMask;

    private bool enemyIsFacingRight = true;

    public bool CanSeePlayer { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVCheck());
    }

    private void Update()
    {
        if (CanSeePlayer)
            enemyIsFacingRight = false; 
        else
            enemyIsFacingRight = true;

        enemySprite.flipX = !enemyIsFacingRight;

        // Flip colliders
        Vector3 scale = enemyPivotPoint.localScale;

        if (enemyIsFacingRight)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        else
        {
            scale.x = -Mathf.Abs(scale.x);
        }

        enemyPivotPoint.localScale = scale;
    }
    private IEnumerator FOVCheck()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            yield return wait;
            FOV();
        }
    }

    private void FOV()
    {
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, radius, targetMask);

        if (rangeCheck.Length > 0)
        {
            Transform target = rangeCheck[0].transform;
            Vector2 directionToTarget = (target.position - transform.position).normalized;

            if (Vector2.Angle(transform.up, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector2.Distance(transform.position, target.position);

                if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    CanSeePlayer = true;
                else
                    CanSeePlayer = false;
            }
            else
            {
                CanSeePlayer = false;
            }
        }
        else if (CanSeePlayer)
        {
            CanSeePlayer = false;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);

        Vector3 angle01 = DrectionFromAngle(-transform.eulerAngles.z, -angle / 2);
        Vector3 angle02 = DrectionFromAngle(transform.eulerAngles.z, angle / 2);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + angle01 * radius);
        Gizmos.DrawLine(transform.position, transform.position + angle02 * radius);

        if (CanSeePlayer)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, playerRef.transform.position);
        }
    }

    private Vector2 DrectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
