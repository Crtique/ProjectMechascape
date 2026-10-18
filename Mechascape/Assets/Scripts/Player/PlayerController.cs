using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    //------------ DECLARE VARIABLES ------------//
    [Header("Player Settings")]
    [SerializeField] private float moveSpeed; 
    [SerializeField] private float jumpHeight;


    [Header("GroundSlamming Control")]
    [SerializeField] private float fallSpeed;
    [SerializeField] private float groundSlamDamage;

    private bool canGroundSlam = true;
    public bool isGroundSlamming;
    private bool hasDealtGroundSlamDamage = false;

    private float groundSlamTime = 0.2f;
    private float groundSlamCoolDown = 1f;


    [Header("Dashing Seetings")]
    [SerializeField] float dashingPower;

    // Dashing Variables
    private bool canDash = true;
    private bool isDashing;

    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;


    [Header("Ground Control")]
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform groundCheck;

    [SerializeField] private HealthSystem health;

    private float moveX;
    private bool isFacingRight = false;

    // --- Objects On player --- //
    private Rigidbody2D rb;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private WeaponKnockback wkb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isDashing == true)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
            health.TakeDamage(10f);

        Inputs();
        FlipSprite();
    }

    //----- HANDLE ALL THE PHYSICS MOVEMENT -----//
    private void FixedUpdate()
    {
        if (isDashing == true)
            return;

        // Affect player with knockback without canceling the normal movement instantly
        if (wkb != null && wkb.IsKnockedBack)
            return;

        // Move Player
        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);
    }

    private bool IsGrounded()
    {
        float radius = 0.2f;

        return Physics2D.OverlapCircle(groundCheck.position, radius, groundMask);
    }

    #region PLAYER CONTROLS

    // Player Inputs Function
    void Inputs()
    {
        // Get X axis inputs
        moveX = Input.GetAxisRaw("Horizontal");

        // Dash Input
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        #region JUMPING INPUTS
        // If pressing space do a small jump
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpHeight);
        }
        #endregion 

        #region GROUNDSLAMMING INPUTS

        // GroundSlamming Input
        if (!IsGrounded() && Input.GetKeyDown(KeyCode.LeftControl) && canGroundSlam && !isGroundSlamming)
        {
            StartCoroutine(GroundSlamming());
        }
        else if (IsGrounded() && isGroundSlamming && !hasDealtGroundSlamDamage)
        {
            hasDealtGroundSlamDamage = true;
            GroundSlammingDamage(groundSlamDamage);
        }
        else if (!IsGrounded() && !isGroundSlamming && hasDealtGroundSlamDamage)
        {
            hasDealtGroundSlamDamage = false;
        }

        #endregion
    }

    //----- FLIP THE DIRECTION OF THE SPRITE BASED ON WHERE IT IS LOOKING -----//
    void FlipSprite()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePos.x > transform.position.x)
            isFacingRight = true;
        else
            isFacingRight = false;

        playerSprite.flipX = !isFacingRight;
    }

    #endregion

    #region DASHING CONTROL

    // Coroutine that manages all of our Dashing control
    private IEnumerator Dash()
    {
        canDash = false;    // Player can longer dash once dashing
        isDashing = true;   // Player now begins to dash

        // Store Orginal Gravity
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        // We check whether out MoveX value is zero or not. If MoveX isn't zero then the player has pressed either A or D
        // Yet when MoveX is zero the player is no longer providing input to the system. Yet we check if we are facing right or not and use those values for our MoveX
        // We will dash based on what our MoveX value becomes from our isFacingRight Boolean Variable
        float dashingDirection = moveX != 0 ? moveX:(isFacingRight ? 1f:-1f);

        rb.linearVelocity = new Vector2(dashingDirection * dashingPower, 0f);

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

    #region GROUNDSLAMMING CONTROL
    
    // Groundslamming Coroutine
    private IEnumerator GroundSlamming()
    {
        canGroundSlam = false;
        isGroundSlamming = true;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);

        rb.AddForce(Vector2.down * fallSpeed, ForceMode2D.Impulse);

        tr.emitting = true;

        yield return new WaitForSeconds(groundSlamTime);
        tr.emitting = false;
        isGroundSlamming = false;

        yield return new WaitForSeconds(groundSlamCoolDown);
        canGroundSlam = true;
    }

    void GroundSlammingDamage(float damage)
    {
        Debug.Log($"GROUND HIT!!! DAMAGE: {damage}");
    }
    
    #endregion
}
