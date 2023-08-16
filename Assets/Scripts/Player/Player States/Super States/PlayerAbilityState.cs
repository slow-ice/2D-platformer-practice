using Assets.Scripts.Refactoring.System.Input_System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState {

    protected bool isAbilityDone;
    protected bool isGrounded;
    protected int xInput;

    public PlayerAbilityState(string animParmName) : base(animParmName) { }

    public override void DoChecks() {
        base.DoChecks();

        isGrounded = core.Sense.GroundCheck;
    }

    public override void OnEnter() {
        base.OnEnter();

        isAbilityDone = false;
    }

    public override void OnExit() {
        base.OnExit();
    }

    public override void OnUpdate() {
        base.OnUpdate();

        xInput = InputManager.Instance.xInput;

        if (isAbilityDone) {
            if (isGrounded && controller.CurrentVelocity.y < 0.1f) {
                stateMachine.ChangeState(controller.GetState<PlayerIdleState>());
            }
            else {
                stateMachine.ChangeState(controller.GetState<PlayerInAirState>());
            }
        }
    }

    public override void OnFixedUpdate() {
        base.OnFixedUpdate();
    }
}
