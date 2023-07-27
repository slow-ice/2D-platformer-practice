using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState {

    public PlayerAttackState(PlayerStateMachine stateMachine, Player player, PlayerData_SO playerData, string animParmName) : base(stateMachine, player, playerData, animParmName) {
    }

    public override void DoCheck() {
        base.DoCheck();
    }

    public override void Enter() {
        base.Enter();
    }

    public override void Exit() {
        base.Exit();
    }

    public override void OnUpdate() {
        base.OnUpdate();
    }
}
