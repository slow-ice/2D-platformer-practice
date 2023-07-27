using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundState {

    private string animName;

    public PlayerLandState(PlayerStateMachine stateMachine, Player player, PlayerData_SO playerData, string animParmName) 
        : base(stateMachine, player, playerData, animParmName) {
        animName = animParmName[..1].ToUpper() + animParmName[1..];
    }

    public override void Enter() {
        base.Enter();
    }

    public override void Exit() {
        base.Exit();
    }

    public override void OnUpdate() {
        base.OnUpdate();

        if (changeToJump) {
            return;
        }

        if (xInput != 0) {
            stateMachine.ChangeState(player.MoveState);
            return;
        }

        if (xInput == 0) {
            player.SetVelocity(0);
        }

        if (player.CheckAnimFinished(animName)) {
            stateMachine.ChangeState(player.IdleState);
        }
    }
}
