using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;

    //Running
    float horizontal;
    public float speed, jump;

    //Jumping
    public Transform groundCheck;
    public LayerMask groundLayer;

    //WallSliding
    public Transform wallCheck;
    public LayerMask wallLayer;
    public float wallSlidingSpeed;
    public bool isFacingRight = true;
    bool isWallSliding;

    //WallJumping
    bool isWallJumping;
    float wallJumpingDirection, wallJumpingCounter;
    float wallJumpingTime = 0.2f, wallJumpingDuration = 0.4f;
    Vector2 wallJumpingPower = new Vector2(20f, 30f);

    //Dash
    public TrailRenderer trail;
    public bool canDash = true;
    bool isDashing;
    public float dashPower;
    float dashTime = 0.2f;
    float dashCD = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAnimation();
        if(isDashing)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Jump"))
        {
            if(IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jump);
            }

        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        WallSlide();
        WallJump();

        if(!isWallJumping)
        {
            Flip();
        }
    
    }

    void FixedUpdate()
    {
        if(isDashing)
        {
            return;
        }
        if(!isWallJumping)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
    }

    void PlayerAnimation()
    {
        Sword sword = gameObject.GetComponent<Sword>();
        if(sword.isAttacking == true)
        {
            anim.SetBool("IsAttacking", true);
        }
        if(sword.isAttacking == false)
        {
            anim.SetBool("IsAttacking", false);
        }
        if(rb.velocity.y != 0)
        {
            anim.SetBool("IsJumping", true);
        }
        if(IsGrounded())
        {
            
            anim.SetBool("IsJumping", false);
            
            if(horizontal == 0)
            {
                anim.SetBool("IsRunning", false);
            }
            else
            {
                anim.SetBool("IsRunning", true);
            }
            
            
            
        }
        else
        {
            anim.SetBool("IsJumping", true);
        }

        if(isDashing == true)
        {
            anim.SetBool("IsDashing", true);
        }
        else if(isDashing == false)
        {
            anim.SetBool("IsDashing", false);
        }

    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    private bool Iswalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    void WallSlide()
    {
        if(Iswalled() && !IsGrounded() && horizontal != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    void WallJump()
    {
        if(isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if(Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if(transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;

                localScale.x *= -1f;
                transform.localScale = localScale;
        
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    void StopWallJumping()
    {
        isWallJumping = false;
    }

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashPower, 0f);
        trail.emitting = true;
        

        yield return new WaitForSeconds(dashTime);

        trail.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashCD);

        canDash = true;
    }

    void Flip()
    {
        if(isFacingRight && horizontal < 0 || !isFacingRight && horizontal > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;

            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Lift")
        {
            transform.parent = collision.gameObject.transform;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Lift")
        {
            transform.parent = null;
        }
    }
}
