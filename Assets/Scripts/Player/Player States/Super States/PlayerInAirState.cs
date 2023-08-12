using Assets.Scripts.Refactoring.System.Input_System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Refactoring {
    public class PlayerInAirState : PlayerStates {

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

        public PlayerInAirState(PlayerStatesEnum stateType, string animName) : base(stateType, animName) {
        }

        public override void PhysicalChecks() {
            base.PhysicalChecks();

            isGrounded = core.CheckIfGrounded();
            //isTouchingWall = core.CheckIfTouchingWall();
            //isTouchingWallBack = core.CheckIfTouchingWallBack();
            //isTouchingEdge = core.CheckIfTouchingEdge();
        }

        public override void OnEnter() {
            base.OnEnter();

            core.mRigidbody.gravityScale = core.PlayerData.gravityScale;
        }

        public override void OnExit() {
            base.OnExit();

            isJumping = false;
            isWallJumping = false;
        }

        public override void OnInit() {
            base.OnInit();
            var transToGround = RegisterTransition(PlayerStatesEnum.land, () => isGrounded && controller.CurrentVelocity.y < 0.01f);

            var transToJump = RegisterTransition(PlayerStatesEnum.jump, () => jumpInput && core.CanJump);

        }

        public override void OnUpdate() {
            base.OnUpdate();

            xInput = InputManager.Instance.xInput;
            jumpInput = InputManager.Instance.JumpInput;
            dashInput = InputManager.Instance.DashInput;

            CheckCoyoteTime();
            JumpExtendedTime();
            MovementInAir();

            //if (isGrounded && controller.CurrentVelocity.y < 0.01f) {
            //    //stateMachine.ChangeState(player.LandState);
            //    return;
            //}

            //// 空中跳跃
            //if (jumpInput && player.JumpState.CanJump) {
            //    player.InputHandler.UseJumpInput();
            //    //stateMachine.ChangeState(player.JumpState);
            //    return;
            //}

            //if (jumpInput && (isTouchingWall || isTouchingWallBack)) {
            //    player.InputHandler.UseJumpInput();
            //    stateMachine.ChangeState(player.WallJumpState);
            //    return;
            //}

            //if (dashInput && player.DashState.CheckIfCanDash()) {
            //    stateMachine.ChangeState(player.DashState);
            //    return;
            //}

            //if (isTouchingWall && !isTouchingEdge) {
            //    player.EdgeState.SetDetectedPos(player.transform.position);
            //    stateMachine.ChangeState(player.EdgeState);
            //    return;
            //}

            //if (isTouchingWall && player.CurrentVelocity.y < 0f && xInput == player.FacingDirection) {
            //    stateMachine.ChangeState(player.WallSlideState);
            //    return;
            //}

            //if (player.InputHandler.AttackInputs[(int)CombatInputs.Primary]) {
            //    stateMachine.ChangeState(player.PrimaryAttackState);
            //    return;
            //}

            //if (player.InputHandler.AttackInputs[(int)CombatInputs.Secondary]) {
            //    stateMachine.ChangeState(player.SecondAttackState);
            //    return;
            //}
        }

        private void JumpExtendedTime() {
            if (isJumping) {
                controller.SetVelocityY(core.PlayerData.jumpForce);
                if (!InputManager.Instance.jumpButtonUp) {
                    InputManager.Instance.jumpHoldTime += Time.deltaTime;
                    if (InputManager.Instance.jumpHoldTime > core.PlayerData.jumpPressedWindow) {
                        isJumping = false;
                        controller.mRigidbody.gravityScale = core.PlayerData.fallGravityScale;
                    }
                }
                else {
                    isJumping = false;
                    controller.mRigidbody.gravityScale = core.PlayerData.fallGravityScale;
                }

                if (controller.CurrentVelocity.y < 0) {
                    isJumping = false;
                    controller.mRigidbody.gravityScale = core.PlayerData.fallGravityScale;
                }
            }
        }

        private void CheckCoyoteTime() {
            if (coyoteTime && Time.time > startTime + core.PlayerData.coyoteTime) {
                coyoteTime = false;
                core.DecreaseJumpLeft();
            }
        }

        private void MovementInAir() {
            core.CheckShouldFlip(xInput);

            var targetVeloX = xInput * core.PlayerData.movementSpeed;

            //// 墙跳 速度为阻尼变化
            //if (isWallJumping) {
            //    targetVeloX = Mathf.Lerp(controller.CurrentVelocity.x, xInput * core.PlayerData.movementSpeed, (float)(Time.deltaTime * 1));
            //}

            controller.SetVelocityX(targetVeloX);

            controller.mAnimator.SetFloat("xVelocity", Mathf.Abs(xInput));
            controller.mAnimator.SetFloat("yVelocity", controller.CurrentVelocity.y);
        }

        public void StartCoyoteTime() => coyoteTime = true;

        public void SetIsJumping() => isJumping = true;

        public void SetIsWallJumping() => isWallJumping = true;
    }
}