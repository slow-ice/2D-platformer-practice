using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEdgeState : PlayerState {
    private Vector2 detectedPos;
    private Vector2 cornerPos;
    private Vector2 startPos;
    private Vector2 endPos;

    private bool isHanging;
    private bool isClimbing;
    private bool jumpInput;

    private int xInput;
    private int yInput;

    public PlayerEdgeState(PlayerStateMachine stateMachine, Player player, PlayerData_SO playerData, string animParmName) : base(stateMachine, player, playerData, animParmName) {
    }

    public override void DoCheck() {
        base.DoCheck();

    }

    public override void Enter() {
        base.Enter();

        player.SetVelocity(0);
        //player.transform.position = detectedPos;

        cornerPos = player.GetCornerPos();

        startPos.Set(cornerPos.x - playerData.startOffset.x * player.FacingDirection, cornerPos.y - playerData.endOffset.y);
        endPos.Set(cornerPos.x + playerData.startOffset.x * player.FacingDirection, cornerPos.y + playerData.endOffset.y);

        player.transform.position = startPos;
    }

    public override void Exit() {
        base.Exit();

        isHanging = false;
        if (isClimbing) {
            isClimbing = false;
            player.transform.position = endPos;
        }
    }

    public override void OnUpdate() {
        base.OnUpdate();

        xInput = player.InputHandler.xInput;
        yInput = player.InputHandler.yInput;
        jumpInput = player.InputHandler.JumpInput;

        if (player.CheckAnimFinished("climbEdge")) {
            player.Animator.SetBool("climbEdge", false);
            stateMachine.ChangeState(player.IdleState);
            return;
        }

        if (jumpInput && !isClimbing) {
            player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.WallJumpState);
            return;
        }

        if (player.AnimationTrigger("holdEdge")) {
            isHanging = true;
        }

        player.transform.position = startPos;
        player.SetVelocity(0);

        if (yInput == -1 && !isClimbing) {
            stateMachine.ChangeState(player.InAirState);
            return;
        }

        if (xInput == player.FacingDirection && isHanging && !isClimbing) {
            isClimbing = true;
            player.Animator.SetBool("climbEdge", true);
        }
    }

    public void SetDetectedPos(Vector2 pos) => detectedPos = pos;
}
