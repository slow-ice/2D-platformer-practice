using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState {
    public PlayerIdleState(PlayerStateMachine stateMachine, Player player,
        PlayerData_SO playerData, string animParmName) 
        : base(stateMachine, player, playerData, animParmName) {

    }

    public override void DoCheck() {
        base.DoCheck();
    }

    public override void Enter() {
        base.Enter();

        player.SetVelocity(0f);
    }

    public override void Exit() {
        base.Exit();
    }

    public override void OnUpdate() {
        base.OnUpdate();

        if (changeToJump) {
            return;
        }

        if (xInput != 0f) {
            stateMachine.ChangeState(player.MoveState);
        }
    }

    public override void OnFixedUpdate() {
        base.OnFixedUpdate();
    }
}
