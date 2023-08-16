using Assets.Scripts.Refactoring.System.Input_System;
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


    public PlayerInAirState(string name) : base(name) { }

    public override void DoChecks() {
        base.DoChecks();

        isGrounded = core.Sense.GroundCheck;
        isTouchingWall = core.Sense.WallCheck;
        isTouchingWallBack = core.Sense.WallBackCheck;
        isTouchingEdge = core.Sense.EdgeCheck;
    }

    public override void OnEnter() {
        base.OnEnter();

        controller.mRigidbody.gravityScale = controller.PlayerData.gravityScale;
    }

    public override void OnExit() {
        base.OnExit();

        isJumping = false;
        isWallJumping = false;
    }

    public override void OnUpdate() {
        base.OnUpdate();

        xInput = InputManager.Instance.xInput;
        jumpInput = InputManager.Instance.JumpInput;
        dashInput = InputManager.Instance.DashInput;

        CheckCoyoteTime();
        JumpExtendedTime();
        MovementInAir();

        if (isGrounded && controller.CurrentVelocity.y < 0.01f) {
            stateMachine.ChangeState(controller.GetState<PlayerLandState>());
            return;
        }

        // 空中跳跃
        if (jumpInput && controller.GetState<PlayerJumpState>().CanJump) {
            InputManager.Instance.UseJumpInput();
            stateMachine.ChangeState(controller.GetState<PlayerJumpState>());
            return;
        }

        if (jumpInput && (isTouchingWall || isTouchingWallBack)) {
            InputManager.Instance.UseJumpInput();
            stateMachine.ChangeState(controller.GetState<PlayerWallJumpState>());
            return;
        }

        if (dashInput && controller.GetState<PlayerDashState>().CheckIfCanDash()) {
            stateMachine.ChangeState(controller.GetState<PlayerDashState>());
            return;
        }

        if (isTouchingWall && !isTouchingEdge) {
            controller.GetState<PlayerEdgeState>().SetDetectedPos(controller.transform.position);
            stateMachine.ChangeState(controller.GetState<PlayerEdgeState>());
            return;
        }

        if (isTouchingWall && controller.CurrentVelocity.y < 0f && xInput == core.FacingDirection) {
            stateMachine.ChangeState(controller.GetState<PlayerWallSlideState>());
            return;
        }

        if (InputManager.Instance.AttackInputs[(int)CombatInputs.Primary]) {
            stateMachine.ChangeState(controller.GetState<PlayerAttackState>());
            return;
        }

        if (InputManager.Instance.AttackInputs[(int)(CombatInputs.Secondary)]) {
            stateMachine.ChangeState(controller.GetState<PlayerAttackState>());
            return;
        }
    }

    private void JumpExtendedTime() {
        if (isJumping) {
            controller.SetVelocityY(controller.PlayerData.jumpForce);
            if (!InputManager.Instance.jumpButtonUp) {
                InputManager.Instance.jumpHoldTime += Time.deltaTime;
                if (InputManager.Instance.jumpHoldTime > controller.PlayerData.jumpPressedWindow) {
                    isJumping = false;
                    controller.mRigidbody.gravityScale = controller.PlayerData.fallGravityScale;
                }
            }
            else {
                isJumping = false;
                controller.mRigidbody.gravityScale = controller.PlayerData.fallGravityScale;
            }

            if (controller.CurrentVelocity.y < 0) {
                isJumping = false;
                controller.mRigidbody.gravityScale = controller.PlayerData.fallGravityScale;
            }
        }
    }

    private void CheckCoyoteTime() {
        if (coyoteTime && Time.time > startTime + controller.PlayerData.coyoteTime) { 
            coyoteTime = false;
            controller.GetState<PlayerJumpState>().DecreaseJumpLeft();
        }
    }

    private void MovementInAir() {
        core.CheckShouldFlip(xInput);

        var targetVeloX = xInput * controller.PlayerData.movementSpeed;

        // 墙跳 速度为阻尼变化
        if (isWallJumping) {
            targetVeloX = Mathf.Lerp(controller.CurrentVelocity.x, xInput * controller.PlayerData.movementSpeed, (float)(Time.deltaTime * 1));
        }
        
        controller.SetVelocityX(targetVeloX);

        controller.mAnimator.SetFloat("xVelocity", Mathf.Abs(xInput));
        controller.mAnimator.SetFloat("yVelocity", controller.CurrentVelocity.y);
    }

    public void StartCoyoteTime() => coyoteTime = true;

    public void SetIsJumping() => isJumping = true;

    public void SetIsWallJumping() => isWallJumping = true;
}
