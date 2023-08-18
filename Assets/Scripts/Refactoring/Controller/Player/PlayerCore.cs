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
    public Animator mAnimator;
    public Rigidbody2D mRigidbody { get; private set; }
    public PlayerData_SO PlayerData;
    public PlayerController mController;

    #region Check Params
    private Transform WallCheck;
    private Transform GroundCheck;
    public SenseController Sense;
    #endregion

    public int FacingDirection = 1;
    public int amountOfJumpLeft;
    public int amountOfJump { get; private set; }

    public PlayerCore InitCore(Transform player) {
        mPlayerTrans = player;
        mController = player.GetComponent<PlayerController>();
        PlayerData = player.GetComponent<PlayerController>().PlayerData;
        mAnimator = player.GetComponent<Animator>();
        mRigidbody = player.GetComponent<Rigidbody2D>();
        Sense = player.GetComponent<SenseController>();
        Sense.SetPlayer(player.GetComponent<PlayerController>());
        InitializeData();
        return this;
    }

    private void InitializeData() {
        amountOfJump = mController.amountOfJump;
    }

    #region Check Funcs
    //public bool CheckIfGrounded() {
    //    return Physics2D.OverlapCircle(GroundCheck.position, PlayerData.GroundCheckRadius, PlayerData.GroundLayer);
    //}

    public void CheckShouldFlip(int xInput) {
        if (xInput != 0 && xInput != FacingDirection) {
            Flip();
        }
    }

    public bool CheckAnimFinished(string animName) {
        var AnimatorInfo = mAnimator.GetCurrentAnimatorStateInfo(0);
        if (AnimatorInfo.IsName(animName) && AnimatorInfo.normalizedTime > 0.99f) {
            Debug.Log($"{animName} finished");
            return true;
        }
        return false;
    }

    public bool AnimationTrigger(string animName) {
        var AnimatorInfo = mAnimator.GetCurrentAnimatorStateInfo(0);
        if (AnimatorInfo.IsName(animName)) {
            return true;
        }
        return false;
    }

    #endregion

    public bool CanJump => amountOfJumpLeft > 0;

    public void ResetJumpLeft() => amountOfJumpLeft = amountOfJump;
    public void DecreaseJumpLeft() => amountOfJumpLeft--;


    public void Flip() {
        FacingDirection *= -1;
        //mPlayerTrans.Rotate(0f, 180f, 0f);
        var scale = mPlayerTrans.localScale;
        scale.x *= -1;
        mPlayerTrans.localScale = scale;
    }

    public IArchitecture GetArchitecture() {
        return GameCenter.Interface;
    }
}
