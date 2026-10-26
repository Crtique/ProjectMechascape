using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Vector3 offset = new Vector3(0, 0, -10f);
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    public Transform player;

    [Header("Look Ahead")]
    [SerializeField] private float lookAheadDistance = 3f;
    [SerializeField] private float lookAheadSpeed = 5f;

    private Vector3 lastPlayerPosition;
    private Vector3 lookAheadOffset;

    void Start()
    {
        lastPlayerPosition = player.position;
    }

    void LateUpdate()
    {
        // Get the direction the player is moving
        Vector3 movement = player.position - lastPlayerPosition;

        Vector3 direction = movement.normalized;

        // Determine where the camera should look ahead
        Vector3 targetLookAhead = direction * lookAheadDistance;

        // Smoothly move the look ahead offset
        lookAheadOffset = Vector3.Lerp(lookAheadOffset, targetLookAhead, lookAheadSpeed * Time.deltaTime);

        // Calculate camera position
        Vector3 targetPosition = player.position + offset + lookAheadOffset;

        // Smooth camera movement
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        lastPlayerPosition = player.position;
    }
}