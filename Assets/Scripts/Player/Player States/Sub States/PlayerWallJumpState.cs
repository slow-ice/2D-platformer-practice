using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerWallJumpState : PlayerAbilityState {

    public PlayerWallJumpState(PlayerStateMachine stateMachine, Player player, PlayerData_SO playerData, string animParmName) : base(stateMachine, player, playerData, animParmName) {
    }

    public override void Enter() {
        base.Enter();

        int direction = player.FacingDirection * -1;
        player.SetVelocity(playerData.wallJumpForce, playerData.wallJumpDirection, direction);
        player.InAirState.SetIsWallJumping();

        player.Flip();
        isAbilityDone = true;
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

    }

    //IEnumerator jumpDelay(float delay) {
    //    yield return new WaitForSeconds(delay);
    //    isAbilityDone = true;
    //}
}
