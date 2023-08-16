using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerWallJumpState : PlayerAbilityState {

    public PlayerWallJumpState(string name) : base(name) { }

    public override void OnEnter() {
        base.OnEnter();

        int direction = core.FacingDirection * -1;
        controller.SetVelocity(controller.PlayerData.wallJumpForce, controller.PlayerData.wallJumpDirection, direction);
        controller.GetState<PlayerInAirState>().SetIsWallJumping();

        core.Flip();
        WaitTimeManager.WaitTime(0.3f, () => isAbilityDone = true);
    }

    public override void OnUpdate() {
        base.OnUpdate();

    }

    //IEnumerator jumpDelay(float delay) {
    //    yield return new WaitForSeconds(delay);
    //    isAbilityDone = true;
    //}
}
