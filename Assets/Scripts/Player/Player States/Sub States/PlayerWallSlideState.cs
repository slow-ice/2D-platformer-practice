using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState {

    private bool isGrounded;

    public PlayerWallSlideState(PlayerStateMachine stateMachine, Player player, PlayerData_SO playerData, string animParmName) : base(stateMachine, player, playerData, animParmName) {
    }

    public override void Enter() {
        base.Enter();
    }

    public override void OnUpdate() {
        base.OnUpdate();

        player.SetVelocityY(-playerData.wallSlideSpeed);

        if (jumpInput) {
            player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.WallJumpState);
            return;
        }

        if (!isTouchingWall) {
            stateMachine.ChangeState(player.InAirState);
            return;
        }

        if (xInput != 0 && xInput != player.FacingDirection) {
            player.CheckShouldFlip(xInput);
            stateMachine.ChangeState(player.InAirState);
            return;
        }
        if (isGrounded) {
            stateMachine.ChangeState(player.IdleState);
            return;
        }
    }

    public override void DoCheck() {
        base.DoCheck();

        isGrounded = player.CheckIfGrounded();
    }
}
