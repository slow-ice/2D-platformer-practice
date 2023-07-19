using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public float movSpeed = 10f;
    private float movDirection;
    public float jumpForce = 1f;
    public float groundCheckRadius = 5f;

    [SerializeField]
    private float jumpPressedWindow = 0.5f;
    private float jumpPressedTime;
    private float gravityScale;
    public float fallGravityScale = 8f;
    public float wallCheckDistance;
    public float wallSlideSpeed;
    public float movForceInAir;
    public float airDragMultiplier;
    public float wallJumpForce;

    [Header("Jump Param")]
    public int amountOfJumps = 1;
    private int leftAmountOfJumps;

    private bool isFacingRight = true;
    private bool isWalking = false;
    private bool isJumping = false;
    private bool isWallJumping;
    private bool isTouchingWall => Physics2D.Raycast(WallCheck.position,
                                    transform.right * (isFacingRight ? 1f : -1f), wallCheckDistance, GroundLayer);
    private bool isWallSliding;
    private bool isGrounded => Physics2D.OverlapCircle(GoundCheck.position, groundCheckRadius, GroundLayer);
    private bool JumpPressed => Input.GetButtonDown("Jump");
    private bool canNormalJump = false;
    private bool canWallJump;

    public Vector2 wallJumpDirection;

    [HideInInspector]public Animator animator;
    private Rigidbody2D rb;
    [SerializeField] private Transform GoundCheck;
    [SerializeField] private Transform WallCheck;
    [SerializeField] private LayerMask GroundLayer;

    private void Awake() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        leftAmountOfJumps = amountOfJumps;
        gravityScale = rb.gravityScale;
        wallJumpDirection.Normalize();
    }

    private void Update() {
        Move();
        SwitchAnim();
        FlipDirection();
        CheckIfCanJump();
        CheckIfWallSliding();
    }

    private void FixedUpdate() {
        ApplyMove();
    }

    private void SwitchAnim() {
        animator.SetBool("Walking", isWalking);
        animator.SetBool("Ground", isGrounded);
        animator.SetFloat("yVelocity", rb.velocity.y);
        animator.SetBool("WallSliding", isWallSliding);
    }

    private void Move() {
        movDirection = Input.GetAxisRaw("Horizontal");
        if (movDirection != 0 && !isWallSliding) {
            isWalking = true;
        }
        else {
            isWalking = false;
        }
    }
    private void CheckIfCanJump() {
        // 落地更新状态
        if (isGrounded && rb.velocity.y < 0) {
            leftAmountOfJumps = amountOfJumps;
            rb.gravityScale = gravityScale;
            canNormalJump = true;
        }
        else {
            canNormalJump = !isWallSliding && leftAmountOfJumps > 0;
        }
        if (isWallSliding) {
            canWallJump = true;
            //leftAmountOfJumps--;
        }
        else {
            canWallJump = false;
        }
        NormalJump();
        WallJump();
    }

    private void CheckIfWallSliding() {
        if (!isGrounded && isTouchingWall && rb.velocity.y < 0) {
            isWallSliding = true;
        }
        else {
            isWallSliding = false;
        }
    }

    private void WallSlide() {
        if (isWallSliding && !isWallJumping) {
            if (rb.velocity.y < -wallSlideSpeed) {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
            }
        }
    }

    private void NormalJump() {
        if (JumpPressed && canNormalJump) {
            //animator.SetTrigger("Jumping");
            leftAmountOfJumps--;
            jumpPressedTime = 0;
            rb.gravityScale = gravityScale;
            isJumping = true;
        }

        //长按继续重置跳跃速度, 实现长按跳高的效果
        if (isJumping) { 
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpPressedTime += Time.deltaTime;
            if (jumpPressedTime > jumpPressedWindow || Input.GetButtonUp("Jump")) {
                isJumping = false;
                rb.gravityScale = fallGravityScale;
            }
            if (rb.velocity.y < 0) {
                isJumping = false;
                rb.gravityScale = fallGravityScale;
            }
        }
    }

    private void WallJump() {
        if (JumpPressed && canWallJump) {
            isWallJumping = true;
            Vector2 dir = new Vector2(wallJumpDirection.x * (isFacingRight ? 1 : -1), wallJumpDirection.y);

            rb.AddForce(dir * wallJumpForce, ForceMode2D.Impulse);
        }
        if (isGrounded) {
            isWallJumping = false;
        }
    }

    private void checkSurroundings() {

    }

    private void FlipDirection() {
        if (!isWallSliding) {
            if (isFacingRight && movDirection < 0) {
                transform.localScale = new Vector3(-1, 1);
                isFacingRight = false;
            }
            if (!isFacingRight && movDirection > 0) {
                transform.localScale = new Vector3(1, 1);
                isFacingRight = true;
            }
        }
    }

    private void ApplyMove() {
        if (isGrounded) {
            rb.velocity = new Vector2(movDirection * movSpeed, rb.velocity.y);
        }
        else if (isWallSliding) {
            WallSlide();
        }
        // 跳跃中移动阻尼
        else if (!isWallSliding && movDirection != 0) {
            Vector2 force = new Vector2(movForceInAir * movDirection, 0);
            rb.AddForce(force);
            if (Mathf.Abs(rb.velocity.x) > movSpeed) {
                rb.velocity = new Vector2(movSpeed * movDirection, rb.velocity.y);
            }
        }
        // 空气阻力
        else if (!isWallSliding && movDirection == 0) {
            rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(GoundCheck.position, groundCheckRadius);
        Gizmos.DrawLine(WallCheck.position, 
            new Vector3(WallCheck.position.x + wallCheckDistance, WallCheck.position.y, WallCheck.position.z));
    }
}
