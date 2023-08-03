using Assets.Scripts.Refactoring;
using Assets.Scripts.Refactoring.FSM.Player_States;
using Assets.Scripts.Refactoring.System.Input_System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerStates {
    public PlayerIdleState(PlayerStatesEnum stateType, string animName) : base(stateType, animName) {
    }

    public override void OnInit() {
        base.OnInit();
        var transToMove = RegisterTransition(PlayerStatesEnum.move);
        transToMove.AddCondition(() => InputManager.Instance.xInput != 0f);
        AddTransition(transToMove);
    }


    public override void OnEnter() {
        base.OnEnter();
        
    }

    public override void OnUpdate() {
        base.OnUpdate();

    }
}
