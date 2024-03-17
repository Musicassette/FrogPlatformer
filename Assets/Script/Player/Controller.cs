using UnityEngine;

public class Controller : MonoBehaviour
{
    public float jumpForce = 8f;
    public float moveSpeed = 5f;
    public float chargeTime = 1f;
    private bool isChargingJump = false;
    private float jumpChargeStartTime;
    private Rigidbody2D rb;
    private bool isOnPlatform = false;
    private bool isJumping = false;
    private float jumpDirection = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleJumpInput();

        if (isOnPlatform && !Input.GetKey(KeyCode.Space))
        {
            HandleHorizontalMovement();
        }
    }

    void HandleJumpInput()
    {
        if (Input.GetButtonDown("Jump") && isOnPlatform && !isChargingJump && !isJumping)
        {
            StartChargingJump();
        }

        if (Input.GetButtonUp("Jump") && isChargingJump)
        {
            ReleaseJump();
        }
    }

    void StartChargingJump()
    {
        isChargingJump = true;
        jumpChargeStartTime = Time.time;
        jumpDirection = Input.GetAxisRaw("Horizontal");
    }

    void ReleaseJump()
    {
        float jumpChargeTime = Time.time - jumpChargeStartTime;
        float jumpForceMultiplier = Mathf.Clamp01(jumpChargeTime / chargeTime);
        Jump(jumpForce * jumpForceMultiplier, jumpDirection);
        isChargingJump = false;
    }

    void HandleHorizontalMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    void Jump(float force, float direction)
    {
        rb.velocity = new Vector2(direction * moveSpeed, force);
        isJumping = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isOnPlatform = true;
            isJumping = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isOnPlatform = false;
        }
    }
}
