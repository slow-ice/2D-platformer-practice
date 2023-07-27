using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundState {
    public PlayerMoveState(PlayerStateMachine stateMachine, Player player, PlayerData_SO playerData, string animParmName) : base(stateMachine, player, playerData, animParmName) {
    }

    public override void DoCheck() {
        base.DoCheck();
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

        player.CheckShouldFlip(xInput);

        player.SetVelocityX(playerData.movementSpeed * xInput);
        if (xInput == 0f) {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void OnFixedUpdate() {
        base.OnFixedUpdate();
    }
}
