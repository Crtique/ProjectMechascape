using System.Collections;
using UnityEngine;

public class PlayerKickController : MonoBehaviour
{
    [SerializeField] private Collider2D kickCollider;

    private readonly float kickCoolDown = 0.5f;
    private readonly float kickDuration = 0.3f;

    private float nextKickTime;
    bool isKicking;

    private void Start()
    {
        kickCollider.enabled = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && Time.time >= nextKickTime)
        {
            StartCoroutine(PerformKick());
        }
    }

    private IEnumerator PerformKick()
    {
        isKicking = true;

        kickCollider.enabled = true;
        Debug.Log("Kick");

        nextKickTime = Time.time + kickCoolDown;

        yield return new WaitForSeconds(kickDuration);

        kickCollider.enabled = false;
        isKicking = false;
        Debug.Log("KickEnd");
    }
}

