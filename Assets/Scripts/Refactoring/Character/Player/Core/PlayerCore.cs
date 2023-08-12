using Assets.Scripts.Refactoring;
using Assets.Scripts.Refactoring.Architecture;
using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCore : IController
{
    private Transform mPlayerTrans;
    private Animator mAnimator;
    public Rigidbody2D mRigidbody { get; private set; }
    public PlayerData_SO PlayerData;

    #region Check Params
    private Transform WallCheck;
    private Transform GroundCheck;
    #endregion

    private int FacingDirection = 1;
    public int amountOfJumpLeft;
    public int amountOfJump { get; private set; }

    public PlayerCore InitCore(Transform player) {
        mPlayerTrans = player;
        PlayerData = player.GetComponent<PlayerController>().PlayerData;
        mAnimator = player.GetComponent<Animator>();
        mRigidbody = player.GetComponent<Rigidbody2D>();
        InitializeData();
        return this;
    }

    private void InitializeData() {
        amountOfJump = PlayerData.amountOfJump;
    }

    public bool CheckIfGrounded() {
        return Physics2D.OverlapCircle(GroundCheck.position, PlayerData.GroundCheckRadius, PlayerData.GroundLayer);
    }

    public void CheckShouldFlip(int xInput) {
        if (xInput != 0 && xInput != FacingDirection) {
            Flip();
        }
    }

    public bool CanJump => amountOfJumpLeft > 0;

    public void ResetJumpLeft() => amountOfJumpLeft = amountOfJump;
    public void DecreaseJumpLeft() => amountOfJumpLeft--;

    public void Flip() {
        FacingDirection *= -1;
        mPlayerTrans.Rotate(0f, 180f, 0f);
    }

    public IArchitecture GetArchitecture() {
        return PlatformerArc.Interface;
    }
}
