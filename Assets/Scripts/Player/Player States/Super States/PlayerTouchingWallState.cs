using Assets.Scripts.Refactoring.System.Input_System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchingWallState : PlayerState {
    protected int xInput;
    protected bool jumpInput;

    protected bool isTouchingWall;


    public PlayerTouchingWallState(string name) : base(name) { }

    public override void DoChecks() {
        base.DoChecks();

        isTouchingWall = core.Sense.WallCheck;
    }

    public override void OnEnter() {
        base.OnEnter();
    }

    public override void OnUpdate() {
        base.OnUpdate();

        xInput = InputManager.Instance.xInput;
        jumpInput = InputManager.Instance.JumpInput;
    }
}
