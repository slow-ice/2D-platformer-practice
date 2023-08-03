using Assets.Scripts.Refactoring.Architecture;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCore : IController
{
    private Transform mPlayerTrans;
    private Animator mAnimator;
    private Rigidbody2D mRigidbody;

    public PlayerCore InitCore(Transform player) {
        mPlayerTrans = player;
        mAnimator = player.GetComponent<Animator>();
        mRigidbody = player.GetComponent<Rigidbody2D>();
        return this;
    }

    public IArchitecture GetArchitecture() {
        return PlatformerArc.Interface;
    }
}
