using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterPlayerController : PlayerController
{
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2.0f;
    [SerializeField] private float coyoteTime = 0.3f;

    private float _lastGroundedTime;
    private float _lastJumpTime;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck = GetComponent<GroundCheck>();
    }

    public override void HandleJump()
    {
        if(!isJumping && (groundCheck.IsGrounded || IsInCoyoteTime() ))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
            isJumping = true;
        }
    }

    protected override void FixedUpdate()
    {
        // Update the lastGrounded time
        if (groundCheck.IsGrounded) _lastGroundedTime = Time.time;
        

        // If you are falling
        if (rb.velocity.y < 0)
        {
            //add more to the existing y velocity
            rb.velocity += Vector2.up * (Physics2D.gravity.y * Time.fixedDeltaTime * fallMultiplier);
        }
        // If you are going up, but not holding the jump button
        else if (rb.velocity.y > 0 && !isJumping)
        {
            rb.velocity += Vector2.up * (Physics2D.gravity.y * Time.fixedDeltaTime * lowJumpMultiplier);
        }
        
        rb.velocity = new Vector2(movementVector.x * speed, rb.velocity.y);
    }

    private bool IsInCoyoteTime()
    {
        // return true if the time difference is less than the grounded time.
        return Time.time - _lastGroundedTime <= coyoteTime;
    }
}
