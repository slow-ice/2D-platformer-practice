using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState {

    private int amountOfJumpLeft;

    public PlayerJumpState(PlayerStateMachine stateMachine, Player player, PlayerData_SO playerData, string animParmName) 
        : base(stateMachine, player, playerData, animParmName) {
        amountOfJumpLeft = player.amountOfJump;
    }

    public override void Enter() {
        base.Enter();

        player.InputHandler.jumpHoldTime = 0;
        player.SetVelocityY(playerData.jumpForce);
        player.InAirState.SetIsJumping();

        DecreaseJumpLeft();
        isAbilityDone = true;
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
    }

    public bool CanJump => amountOfJumpLeft > 0;

    public void DecreaseJumpLeft() => amountOfJumpLeft--;

    public void ResetJumpLeft() => amountOfJumpLeft = player.amountOfJump;
}
