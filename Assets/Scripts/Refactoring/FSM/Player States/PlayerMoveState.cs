using Assets.Scripts.Refactoring;
using Assets.Scripts.Refactoring.System.Input_System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerMoveState : PlayerGroundState {
    public PlayerMoveState(PlayerStatesEnum stateType, string animName) : base(stateType, animName) {
    }

    public override void OnInit() {
        base.OnInit();
        RegisterTransition(PlayerStatesEnum.idle, () => InputManager.Instance.xInput == 0);
    }

    public override void OnUpdate() {
        base.OnUpdate();

        //if (changeToJump) {
        //    return;
        //}

        //player.CheckShouldFlip(xInput);

        //player.SetVelocityX(playerData.movementSpeed * xInput);
        //if (xInput == 0f) {
        //    stateMachine.ChangeState(player.IdleState);
        //}

        core.CheckShouldFlip(InputManager.Instance.xInput);
        controller.SetVelocityX(core.PlayerData.movementSpeed * InputManager.Instance.xInput);
    }

    public override void OnFixedUpdate() {
        base.OnFixedUpdate();
    }
}
