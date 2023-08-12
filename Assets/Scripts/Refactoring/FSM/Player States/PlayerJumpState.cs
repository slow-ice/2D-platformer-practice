using Assets.Scripts.Refactoring;
using Assets.Scripts.Refactoring.System.Input_System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerStates {

    public PlayerJumpState(PlayerStatesEnum stateType, string animName) : base(stateType, animName) {
    }

    public override void OnInit() {
        base.OnInit();
        core.amountOfJumpLeft = core.amountOfJump;
        var transition = RegisterTransition(PlayerStatesEnum.inAir, () => true);
    }

    public override void OnEnter() {
        base.OnEnter();

        InputManager.Instance.jumpHoldTime = 0;
        controller.SetVelocityY(core.PlayerData.jumpForce);
        player.InAirState.SetIsJumping();

        DecreaseJumpLeft();
        isAbilityDone = true;
    }

    public override void OnUpdate() {
        base.OnUpdate();
    }

}
