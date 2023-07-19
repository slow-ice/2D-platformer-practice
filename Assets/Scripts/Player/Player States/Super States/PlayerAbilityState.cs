using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState {

    protected bool isAbilityDone;
    protected bool isGrounded;
    protected int xInput;

    public PlayerAbilityState(PlayerStateMachine stateMachine, Player player, PlayerData_SO playerData, string animParmName) : base(stateMachine, player, playerData, animParmName) {
    }

    public override void DoCheck() {
        base.DoCheck();

        isGrounded = player.CheckIfGrounded();
    }

    public override void Enter() {
        base.Enter();

        isAbilityDone = false;
    }

    public override void Exit() {
        base.Exit();
    }

    public override void LogicUpdate() {
        base.LogicUpdate();

        xInput = player.InputHandler.xInput;

        if (isAbilityDone) {
            if (isGrounded && player.CurrentVelocity.y < 0.1f) {
                stateMachine.ChangeState(player.IdleState);
            }
            else {
                stateMachine.ChangeState(player.InAirState);
            }
        }
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
    }
}
