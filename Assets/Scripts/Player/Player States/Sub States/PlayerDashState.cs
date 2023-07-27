using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState {

    private Vector2 dashDirectionInput;
    private Vector2 dashDirection;

    public bool CanDash {  get; private set; }
    private bool dashInput;
    private bool isHolding;
    private bool dashInputStop;

    private float lastDashTime = -100f;

    public PlayerDashState(PlayerStateMachine stateMachine, Player player, PlayerData_SO playerData, string animParmName) : base(stateMachine, player, playerData, animParmName) {
    }

    public override void DoCheck() {
        base.DoCheck();
    }

    public override void Enter() {
        base.Enter();

        player.InputHandler.UseDashInput();
        CanDash = false;
        isHolding = true;

        dashDirection = Vector2.right * player.FacingDirection;

        Time.timeScale = playerData.dashTimeScale;
        startTime = Time.unscaledTime;

        player.DashIndicator.gameObject.SetActive(true);
    }

    public override void Exit() {
        base.Exit();

        isHolding = false;
        Time.timeScale = 1;
        player.DashIndicator.gameObject.SetActive(false);

        if (player.CurrentVelocity.y > 0) {
            player.SetVelocityY(player.CurrentVelocity.y * playerData.dashEndMultiplier);
        }
    }

    public override void OnUpdate() {
        base.OnUpdate();

        if (isAbilityDone) {
            return;
        }

        SetMovementAnim();

        if (isHolding) {
            dashDirectionInput = player.InputHandler.DashDirectionInput;
            dashInput = player.InputHandler.DashInput;
            dashInputStop = player.InputHandler.DashButtonUp;

            if (dashDirectionInput != Vector2.zero) {
                dashDirection = dashDirectionInput;
            }

            var angle = Vector2.SignedAngle(Vector2.right, dashDirection);
            player.DashIndicator.rotation = Quaternion.Euler(0, 0, angle - 90);

            if (dashInputStop || (Time.unscaledTime > startTime + playerData.dashMaxHoldTime)) {
                isHolding = false;
                Time.timeScale = 1f;
                startTime = Time.time;
                player.DashIndicator.gameObject.SetActive(false);
                player.SetVelocity(playerData.dashVelocity, dashDirection, 1);
            }
        }
        else {
            player.SetVelocity(playerData.dashVelocity, dashDirection, 1);
            player.CheckShouldFlip((int)Math.Clamp(player.CurrentVelocity.x, -1, 1));


            if (Time.time > startTime + playerData.dashTime) {
                player.RB.drag = 0;
                lastDashTime = Time.time;
                isAbilityDone = true;
            }
        }
    }

    private void SetMovementAnim() {
        player.Animator.SetFloat("yVelocity", player.CurrentVelocity.y);
        player.Animator.SetFloat("xVelocity", Mathf.Abs(player.CurrentVelocity.x));
    }

    public bool CheckIfCanDash() {
        return CanDash && Time.time >= lastDashTime + playerData.dashCoolDown;
    }

    public void ResetCanDash() => CanDash = true;
}
