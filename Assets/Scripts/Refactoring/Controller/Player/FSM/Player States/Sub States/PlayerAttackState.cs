using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState {

    public PlayerAttackState(string name) : base(name) { }

    public override void DoChecks() {
        base.DoChecks();
    }

    public override void OnEnter() {
        base.OnEnter();
        controller.SetVelocity(0);
        stateMachine.ChangeState(controller.GetState<PlayerIdleState>());
    }

    public override void OnExit() {
        base.OnExit();
    }

    public override void OnUpdate() {
        base.OnUpdate();
    }
}
