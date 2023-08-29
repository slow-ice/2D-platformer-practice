using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState {
    public PlayerIdleState(string animName) : base(animName) { }

    public override void DoChecks() {
        base.DoChecks();
    }

    public override void OnEnter() {
        base.OnEnter();

        controller.SetVelocityToZero(0f);
    }

    public override void OnExit() {
        base.OnExit();
    }

    public override void OnUpdate() {
        base.OnUpdate();

        if (changeToJump) {
            return;
        }

        if (xInput != 0f) {
            stateMachine.ChangeState(controller.GetState<PlayerMoveState>());
        }
    }

    public override void OnFixedUpdate() {
        base.OnFixedUpdate();
    }
}
