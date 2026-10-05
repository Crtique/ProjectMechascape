using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    //------------ DECLARE VARIABLES ------------//
    [Header("Player Settings")]
    [SerializeField] float moveSpeed; 
    [SerializeField] float jumpHeight;

    [Header("Dashing Seetings")]
    [SerializeField] float dashingPower;

    // Dashing Variables
    public bool canDash = true;
    public bool isDashing;

    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    [Header("Ground Control")]
    [SerializeField] LayerMask groundMask;
    [SerializeField] Transform groundCheck;

    float moveX;
    bool isFacingRight = false;

    private Rigidbody2D rb;
    [SerializeField] TrailRenderer tr;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        DashLockState();

        Inputs();
        FlipSprite();
    }

    //----- HANDLE ALL THE PHYSICS MOVEMENT -----//
    private void FixedUpdate()
    {
        DashLockState();

        // Move Player
        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);
    }

    private bool IsGrounded()
    {
        float radius = 0.2f;

        return Physics2D.OverlapCircle(groundCheck.position, radius, groundMask);
    }

    #region PLAYER CONTROLS

    void Inputs()
    {
        // Get X axis inputs
        moveX = Input.GetAxisRaw("Horizontal");

        #region JUMPING INPUTS
        // If pressing space do a small jump
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpHeight);
        }

        // If still holding space key jump higher
        else if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }
        #endregion

        // Dash Input
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    //----- FLIP THE DIRECTION OF THE SPRITE BASED ON WHERE IT IS LOOKING -----//
    void FlipSprite()
    {
        if (isFacingRight && moveX < 0f || !isFacingRight && moveX > 0f)
        {
            isFacingRight = !isFacingRight;

            // Store our localscale
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    #endregion

    #region DASHING CONTROL

    // Prevent player from moving and jumping while dashing
    // we will us a Function call to make this simpler
    void DashLockState()
    {
        if (isDashing == true)
        {
            return;
        }
    }

    // Coroutine that manages all of our Dashing control
    private IEnumerator Dash()
    {
        canDash = false;    // Player can longer dash once dashing
        isDashing = true;   // Player now begins to dash

        // Store Orginal Gravity
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(transform.localScale.x * dashingPower, 0f);

        tr.emitting = true;

        // Stop dashing after waiting for a short ammount of time then reset our gravity
        yield return new WaitForSeconds(dashingTime);

        tr.emitting = false;

        rb.gravityScale = originalGravity;
        isDashing = false;

        // Wait for another short ammount of time and then allow the player to dash again
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    #endregion
}
