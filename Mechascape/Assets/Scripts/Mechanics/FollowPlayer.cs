using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Camera and mouse variables
    private Camera mainCamera;
    private Vector3 mousePos;

    // Camera offsets and timing variables
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
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void LateUpdate()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = player.position.z;

        Vector3 direction = (mousePos - player.position).normalized;

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