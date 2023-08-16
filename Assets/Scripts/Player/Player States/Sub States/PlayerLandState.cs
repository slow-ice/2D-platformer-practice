using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundState {

    private string animName;

    public PlayerLandState(string animName) : base(animName) { }

    public override void OnEnter() {
        base.OnEnter();
    }

    public override void OnExit() {
        base.OnExit();
    }

    public override void OnUpdate() {
        base.OnUpdate();

        if (changeToJump) {
            return;
        }

        if (xInput != 0) {
            stateMachine.ChangeState(controller.GetState<PlayerMoveState>());
            return;
        }

        if (xInput == 0) {
            controller.SetVelocity(0);
        }

        if (core.CheckAnimFinished(animName)) {
            stateMachine.ChangeState(controller.GetState<PlayerIdleState>());
        }
    }
}
