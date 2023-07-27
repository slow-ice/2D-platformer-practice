using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState {

    private bool isGrounded;
    private bool jumpInput;
    private bool coyoteTime;
    private bool isJumping;
    private bool isTouchingWall;
    private bool isTouchingWallBack;
    private bool isWallJumping;
    private bool isTouchingEdge;
    private bool dashInput;

    private int xInput;


    public PlayerInAirState(PlayerStateMachine stateMachine, Player player, PlayerData_SO playerData, string animParmName) : base(stateMachine, player, playerData, animParmName) {
    }

    public override void DoCheck() {
        base.DoCheck();

        isGrounded = player.CheckIfGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
        isTouchingWallBack = player.CheckIfTouchingWallBack();
        isTouchingEdge = player.CheckIfTouchingEdge();
    }

    public override void Enter() {
        base.Enter();

        player.RB.gravityScale = playerData.gravityScale;
    }

    public override void Exit() {
        base.Exit();

        isJumping = false;
        isWallJumping = false;
    }

    public override void OnUpdate() {
        base.OnUpdate();

        xInput = player.InputHandler.xInput;
        jumpInput = player.InputHandler.JumpInput;
        dashInput = player.InputHandler.DashInput;

        CheckCoyoteTime();
        JumpExtendedTime();
        MovementInAir();

        if (isGrounded && player.CurrentVelocity.y < 0.01f) {
            stateMachine.ChangeState(player.LandState);
            return;
        }

        // 空中跳跃
        if (jumpInput && player.JumpState.CanJump) {
            player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
            return;
        }

        if (jumpInput && (isTouchingWall || isTouchingWallBack)) {
            player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.WallJumpState);
            return;
        }

        if (dashInput && player.DashState.CheckIfCanDash()) {
            stateMachine.ChangeState(player.DashState);
            return;
        }

        if (isTouchingWall && !isTouchingEdge) {
            player.EdgeState.SetDetectedPos(player.transform.position);
            stateMachine.ChangeState(player.EdgeState);
            return;
        }

        if (isTouchingWall && player.CurrentVelocity.y < 0f && xInput == player.FacingDirection) {
            stateMachine.ChangeState(player.WallSlideState);
            return;
        }

        if (player.InputHandler.AttackInputs[(int)CombatInputs.Primary]) {
            stateMachine.ChangeState(player.PrimaryAttackState);
            return;
        }

        if (player.InputHandler.AttackInputs[(int)(CombatInputs.Secondary)]) {
            stateMachine.ChangeState(player.SecondAttackState);
            return;
        }
    }

    private void JumpExtendedTime() {
        if (isJumping) {
            player.SetVelocityY(playerData.jumpForce);
            if (!player.InputHandler.jumpButtonUp) {
                player.InputHandler.jumpHoldTime += Time.deltaTime;
                if (player.InputHandler.jumpHoldTime > playerData.jumpPressedWindow) {
                    isJumping = false;
                    player.RB.gravityScale = playerData.fallGravityScale;
                }
            }
            else {
                isJumping = false;
                player.RB.gravityScale = playerData.fallGravityScale;
            }

            if (player.CurrentVelocity.y < 0) {
                isJumping = false;
                player.RB.gravityScale = playerData.fallGravityScale;
            }
        }
    }

    private void CheckCoyoteTime() {
        if (coyoteTime && Time.time > startTime + playerData.coyoteTime) { 
            coyoteTime = false;
            player.JumpState.DecreaseJumpLeft();
        }
    }

    private void MovementInAir() {
        player.CheckShouldFlip(xInput);

        var targetVeloX = xInput * playerData.movementSpeed;

        // 墙跳 速度为阻尼变化
        if (isWallJumping) {
            targetVeloX = Mathf.Lerp(player.CurrentVelocity.x, xInput * playerData.movementSpeed, (float)(Time.deltaTime * 1));
        }
        
        player.SetVelocityX(targetVeloX);

        player.Animator.SetFloat("xVelocity", Mathf.Abs(xInput));
        player.Animator.SetFloat("yVelocity", player.CurrentVelocity.y);
    }

    public void StartCoyoteTime() => coyoteTime = true;

    public void SetIsJumping() => isJumping = true;

    public void SetIsWallJumping() => isWallJumping = true;
}
