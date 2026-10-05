using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float speed = 3f;
    [SerializeField] private int startDirection = 1;
    private int currentDirection;
    private float halfWidth;
    private Vector2 movement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        halfWidth = spriteRenderer.bounds.extents.x;
        currentDirection = startDirection;
    }

    // Allows the enemy to start moving
    private void FixedUpdate()
    {
        movement.x = speed * currentDirection;
        movement.y = rigidBody.linearVelocity.y;
        rigidBody.linearVelocity = movement;
        SetDirection();
    }

    // Changes the enemy direction if they collide with a wall
    private void SetDirection()
    {
        // Checks if there is a wall to the right of the enemy along with if the enemy is going right. IF BOTH ARE TRUE, the enemy changes direction and goes left
        // Without this, the enemy will constantly go right regardless of if it can't anymore
        if (Physics2D.Raycast(transform.position, Vector2.right, halfWidth + 0.1f, LayerMask.GetMask("Ground")) && rigidBody.linearVelocity.x > 0)
        {
            currentDirection *= -1;
        }

        // Checks if there is a wall to the left of the enemy along with if the enemy is going left. IF BOTH ARE TRUE, the enemy changes direction and goes right
        // Without this, the enemy will constantly go left regardless of if it can't anymore
        else if (Physics2D.Raycast(transform.position, Vector2.left, halfWidth + 0.1f, LayerMask.GetMask("Ground")) && rigidBody.linearVelocity.x < 0)
        {
            currentDirection *= -1;
        }
    }
}
