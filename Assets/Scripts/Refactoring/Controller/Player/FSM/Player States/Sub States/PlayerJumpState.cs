using Assets.Scripts.Refactoring.System.Input_System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState {

    private int amountOfJumpLeft;

    public PlayerJumpState(string name) : base(name) { }

    public override void OnEnter() {
        base.OnEnter();

        InputManager.Instance.jumpHoldTime = 0;
        controller.SetVelocityY(controller.PlayerData.jumpForce);
        controller.GetState<PlayerInAirState>().SetIsJumping();

        DecreaseJumpLeft();
        isAbilityDone = true;
    }

    public override void OnUpdate() {
        base.OnUpdate();
    }

    public bool CanJump => amountOfJumpLeft > 0;

    public void DecreaseJumpLeft() => amountOfJumpLeft--;

    public void ResetJumpLeft() => amountOfJumpLeft = controller.amountOfJump;
}
