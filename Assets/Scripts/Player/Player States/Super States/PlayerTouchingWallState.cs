using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchingWallState : PlayerState {
    protected int xInput;
    protected bool jumpInput;

    protected bool isTouchingWall;


    public PlayerTouchingWallState(PlayerStateMachine stateMachine, Player player, PlayerData_SO playerData, string animParmName) : base(stateMachine, player, playerData, animParmName) {
    }

    public override void DoCheck() {
        base.DoCheck();

        isTouchingWall = player.CheckIfTouchingWall();
    }

    public override void Enter() {
        base.Enter();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        xInput = player.InputHandler.xInput;
        jumpInput = player.InputHandler.JumpInput;
    }
}
