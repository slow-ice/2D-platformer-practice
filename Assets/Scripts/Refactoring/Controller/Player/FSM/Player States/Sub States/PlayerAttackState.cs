using Assets.Scripts.Refactoring.Controller.Weapon;
using Assets.Scripts.Refactoring.Model.Weapon;
using Assets.Scripts.Refactoring.System.Input_System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState {

    private WeaponController weaponController;

    private int attackCount;

    private float lastAttackTime;

    private bool changeToNext;

    public PlayerAttackState(string name) : base(name) {
        attackCount = 0;
    }

    public override void DoChecks() {
        base.DoChecks();
    }

    public override void OnEnter() {
        base.OnEnter();
        controller.SetVelocityToZero(0);
        //stateMachine.ChangeState(controller.GetState<PlayerIdleState>());

        if (attackCount >= 3) {
            attackCount = 0;
        }
        else if (lastAttackTime + (weaponController.CurrentWeapon.weaponData as SwordData_SO).attackCoyoteTime < Time.time) {
            attackCount = 0;
        }

        SetAttackMove();
        changeToNext = false;
        weaponController.PlayAnim(attackCount);
    }

    public override void OnExit() {
        base.OnExit();

        weaponController.StopAnim();
        attackCount++;
        lastAttackTime = Time.time;
    }

    public override void OnUpdate() {
        base.OnUpdate();

        if (weaponController.IsAnimationOver()) {
            if (!changeToNext)
                isAbilityDone = true;

            if (changeToNext) {
                UseAttackInput();
                stateMachine.ChangeState(controller.GetState<PlayerAttackState>());
            }
        }
        else if (InputManager.Instance.Attack) {
            changeToNext = true;
        }
    }

    public void UseAttackInput() {
        InputManager.Instance.Attack = false;
    }

    private void SetAttackMove() {
        var speed = (weaponController.CurrentWeapon.weaponData as SwordData_SO).moveSpeedList[attackCount];
        controller.SetVelocityX(speed * core.FacingDirection);
    }

    public bool IsLastHit() {
        return attackCount >= 2;
    }

    public void SetWeaponController(WeaponController weaponController) {
        this.weaponController = weaponController;
    }
}
