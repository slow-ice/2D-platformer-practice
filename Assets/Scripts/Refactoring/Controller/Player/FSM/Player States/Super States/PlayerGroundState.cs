using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Refactoring;
using Assets.Scripts.Refactoring.Controller.Player.FSM.Player_States.Sub_States;
using Assets.Scripts.Refactoring.System.Input_System;
using UnityEngine;


public class PlayerGroundState : PlayerState {

    protected int xInput;

    private bool jumpInput;
    private bool isGrounded;
    protected bool changeToJump;
    private bool dashInput;

    protected bool isStateOver;

    public PlayerGroundState(string animName) :base(animName) { }

    public override void DoChecks() {
        base.DoChecks();

        isGrounded = core.Sense.GroundCheck;
    }

    public override void OnEnter() {
        base.OnEnter();

        changeToJump = false;
        isStateOver = false;

        controller.GetState<PlayerJumpState>().ResetJumpLeft();
        controller.GetState<PlayerDashState>().ResetCanDash();
    }

    public override void OnExit() {
        base.OnExit();
    }

    public override void OnUpdate() {
        base.OnUpdate();

        xInput = InputManager.Instance.xInput;
        jumpInput = InputManager.Instance.JumpInput;
        dashInput = InputManager.Instance.DashInput;

        if (InputManager.Instance.AttackInputs[(int)CombatInputs.Primary]) {
            isStateOver = true;
            controller.GetState<PlayerAttackState>().UseAttackInput();
            stateMachine.ChangeState(controller.GetState<PlayerAttackState>());
            return;
        }

        if (InputManager.Instance.AttackInputs[(int)(CombatInputs.Secondary)]) {
            isStateOver = true;
            stateMachine.ChangeState(controller.GetState<PlayerAttackState>());
            return;
        }

        if (core.IsHurt) {
            stateMachine.ChangeState(controller.GetState<PlayerHurtState>());
            return;
        }

        if (jumpInput && controller.GetState<PlayerJumpState>().CanJump) {
            changeToJump = true;
            InputManager.Instance.UseJumpInput();
            stateMachine.ChangeState(controller.GetState<PlayerJumpState>());
            return;
        }

        if (!isGrounded) {
            controller.GetState<PlayerInAirState>().StartCoyoteTime();
            stateMachine.ChangeState(controller.GetState<PlayerInAirState>());
            return;
        }

        if (dashInput && controller.GetState<PlayerDashState>().CheckIfCanDash()) {
            stateMachine.ChangeState(controller.GetState<PlayerDashState>());
            return;
        }
    }

    public override void OnFixedUpdate() {
        base.OnFixedUpdate();
    }
}

