using Assets.Scripts.Refactoring.System.Input_System;
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

    public PlayerDashState(string animName) : base(animName) { }

    public override void DoChecks() {
        base.DoChecks();
    }

    public override void OnEnter() {
        base.OnEnter();

        InputManager.Instance.UseDashInput();
        CanDash = false;
        isHolding = true;

        dashDirection = Vector2.right * core.FacingDirection;

        Time.timeScale = controller.PlayerData.dashTimeScale;
        startTime = Time.unscaledTime;

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);

        controller.DashIndicator.gameObject.SetActive(true);
    }

    public override void OnExit() {
        base.OnExit();

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);

        isHolding = false;
        Time.timeScale = 1;
        controller.DashIndicator.gameObject.SetActive(false);

        if (controller.CurrentVelocity.y > 0) {
            controller.SetVelocityY(controller.CurrentVelocity.y * controller.PlayerData.dashEndMultiplier);
        }
    }

    public override void OnUpdate() {
        base.OnUpdate();

        if (isAbilityDone) {
            return;
        }

        SetMovementAnim();

        if (isHolding) {
            dashDirectionInput = InputManager.Instance.DashDirectionInput;
            dashInput = InputManager.Instance.DashInput;
            dashInputStop = InputManager.Instance.DashButtonUp;

            if (dashDirectionInput != Vector2.zero) {
                dashDirection = dashDirectionInput;
            }

            var angle = Vector2.SignedAngle(Vector2.right, dashDirection);
            controller.DashIndicator.rotation = Quaternion.Euler(0, 0, angle - 90);

            if (dashInputStop || (Time.unscaledTime > startTime + controller.PlayerData.dashMaxHoldTime)) {
                isHolding = false;
                Time.timeScale = 1f;
                startTime = Time.time;
                controller.DashIndicator.gameObject.SetActive(false);
                controller.SetVelocity(controller.PlayerData.dashVelocity, dashDirection, 1);
            }
        }
        else {
            controller.SetVelocity(controller.PlayerData.dashVelocity, dashDirection, 1);
            core.CheckShouldFlip((int)Math.Clamp(controller.CurrentVelocity.x, -1, 1));

            if (Time.time > startTime + controller.PlayerData.dashTime) {
                controller.mRigidbody.drag = 0;
                lastDashTime = Time.time;
                isAbilityDone = true;
            }
        }
    }

    private void SetMovementAnim() {
        controller.mAnimator.SetFloat("yVelocity", controller.CurrentVelocity.y);
        controller.mAnimator.SetFloat("xVelocity", Mathf.Abs(controller.CurrentVelocity.x));
    }

    public bool CheckIfCanDash() {
        return CanDash && Time.time >= lastDashTime + controller.PlayerData.dashCoolDown;
    }

    public void ResetCanDash() => CanDash = true;
}
