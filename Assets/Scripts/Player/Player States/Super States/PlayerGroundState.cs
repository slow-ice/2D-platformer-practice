using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerGroundState : PlayerState {

    protected int xInput;

    private bool jumpInput;
    private bool isGrounded;
    protected bool changeToJump;
    private bool dashInput;

    public PlayerGroundState(PlayerStateMachine stateMachine, Player player, PlayerData_SO playerData, string animParmName) : base(stateMachine, player, playerData, animParmName) {
    }

    public override void DoCheck() {
        base.DoCheck();

        isGrounded = player.CheckIfGrounded();
    }

    public override void Enter() {
        base.Enter();

        changeToJump = false;
        player.JumpState.ResetJumpLeft();
        player.DashState.ResetCanDash();
    }

    public override void Exit() {
        base.Exit();
    }

    public override void OnUpdate() {
        base.OnUpdate();

        xInput = player.InputHandler.xInput;
        jumpInput = player.InputHandler.JumpInput;
        dashInput = player.InputHandler.DashInput;

        if (player.InputHandler.AttackInputs[(int)CombatInputs.Primary]) {
            stateMachine.ChangeState(player.PrimaryAttackState);
            return;
        }

        if (player.InputHandler.AttackInputs[(int)(CombatInputs.Secondary)]) {
            stateMachine.ChangeState(player.SecondAttackState); 
            return;
        }

        if (jumpInput && player.JumpState.CanJump) {
            changeToJump = true;
            player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
            return;
        }

        if (!isGrounded) {
            player.InAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.InAirState);
            return;
        }

        if (dashInput && player.DashState.CheckIfCanDash()) {
            stateMachine.ChangeState(player.DashState);
            return;
        }
    }

    public override void OnFixedUpdate() {
        base.OnFixedUpdate();
    }
}

