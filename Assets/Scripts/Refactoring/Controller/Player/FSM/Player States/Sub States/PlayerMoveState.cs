using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundState {
    public PlayerMoveState(string name) : base(name) { }

    public override void DoChecks() {
        base.DoChecks();
    }

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
        if (isStateOver) {
            return;
        }

        core.CheckShouldFlip(xInput);

        controller.SetVelocityX(controller.PlayerData.movementSpeed * xInput);
        if (xInput == 0f) {
            stateMachine.ChangeState(controller.GetState<PlayerIdleState>());
        }
    }

    public override void OnFixedUpdate() {
        base.OnFixedUpdate();
    }
}
