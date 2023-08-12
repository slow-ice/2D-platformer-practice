using Assets.Scripts.Refactoring;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundState {

    private string animName;

    public PlayerLandState(PlayerStatesEnum stateType, string animName) : base(stateType, animName) {
    }

    public override void OnUpdate() {
        base.OnUpdate();

        //if (changeToJump) {
        //    return;
        //}

        //if (xInput != 0) {
        //    stateMachine.ChangeState(player.MoveState);
        //    return;
        //}

        //if (xInput == 0) {
        //    player.SetVelocity(0);
        //}

        //if (player.CheckAnimFinished(animName)) {
        //    stateMachine.ChangeState(player.IdleState);
        //}
    }
}
