using Assets.Scripts.Refactoring.System.Input_System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState {

    private bool isGrounded;

    public PlayerWallSlideState(string name) : base(name) { }

    public override void OnEnter() {
        base.OnEnter();
    }

    public override void OnUpdate() {
        base.OnUpdate();

        controller.SetVelocityY(-controller.PlayerData.wallSlideSpeed);

        if (jumpInput) {
            InputManager.Instance.UseJumpInput();
            stateMachine.ChangeState(controller.GetState<PlayerWallJumpState>());
            return;
        }

        if (!isTouchingWall) {
            stateMachine.ChangeState(controller.GetState<PlayerInAirState>());
            return;
        }

        if (xInput != 0 && xInput != core.FacingDirection) {
            core.CheckShouldFlip(xInput);
            stateMachine.ChangeState(controller.GetState<PlayerInAirState>());
            return;
        }
        if (isGrounded) {
            stateMachine.ChangeState(controller.GetState<PlayerIdleState>());
            return;
        }
    }

    public override void DoChecks() {
        base.DoChecks();

        isGrounded = core.Sense.GroundCheck;
    }
}
