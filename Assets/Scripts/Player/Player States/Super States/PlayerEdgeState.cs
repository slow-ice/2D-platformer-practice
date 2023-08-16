using Assets.Scripts.Refactoring.System.Input_System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEdgeState : PlayerState {
    private Vector2 detectedPos;
    private Vector2 cornerPos;
    private Vector2 startPos;
    private Vector2 endPos;

    private bool isHanging;
    private bool isClimbing;
    private bool jumpInput;

    private int xInput;
    private int yInput;

    public PlayerEdgeState(string name) : base(name) { }

    public override void DoChecks() {
        base.DoChecks();

    }

    public override void OnEnter() {
        base.OnEnter();

        controller.SetVelocity(0);
        controller.transform.position = detectedPos;

        cornerPos = controller.GetCornerPos();

        startPos.Set(cornerPos.x - controller.PlayerData.startOffset.x * core.FacingDirection, cornerPos.y - controller.PlayerData.endOffset.y);
        endPos.Set(cornerPos.x + controller.PlayerData.startOffset.x * core.FacingDirection, cornerPos.y + controller.PlayerData.endOffset.y);

        controller.transform.position = startPos;
    }

    public override void OnExit() {
        base.OnExit();

        isHanging = false;
        if (isClimbing) {
            isClimbing = false;
            controller.transform.position = endPos;
        }
    }

    public override void OnUpdate() {
        base.OnUpdate();

        xInput = InputManager.Instance.xInput;
        yInput = InputManager.Instance.yInput;
        jumpInput = InputManager.Instance.JumpInput;

        if (core.CheckAnimFinished("climbEdge")) {
            controller.mAnimator.SetBool("climbEdge", false);
            stateMachine.ChangeState(controller.GetState<PlayerIdleState>());
            return;
        }

        if (jumpInput && !isClimbing) {
            InputManager.Instance.UseJumpInput();
            stateMachine.ChangeState(controller.GetState<PlayerWallJumpState>());
            return;
        }

        if (core.AnimationTrigger("holdEdge")) {
            isHanging = true;
        }

        controller.transform.position = startPos;
        controller.SetVelocity(0);

        if (yInput == -1 && !isClimbing) {
            stateMachine.ChangeState(controller.GetState<PlayerInAirState>());
            return;
        }

        if (xInput == core.FacingDirection && isHanging && !isClimbing) {
            isClimbing = true;
            controller.mAnimator.SetBool("climbEdge", true);
        }
    }

    public void SetDetectedPos(Vector2 pos) => detectedPos = pos;
}
