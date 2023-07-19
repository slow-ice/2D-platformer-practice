using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    protected PlayerData_SO playerData;

    protected float startTime;

    protected string animParmName;

    public PlayerState(PlayerStateMachine stateMachine, Player player, PlayerData_SO playerData, string animParmName) {
        this.stateMachine = stateMachine;
        this.player = player;
        this.playerData = playerData;
        this.animParmName = animParmName;
    }

    public virtual void Enter() {
        DoCheck();
        startTime = Time.time;
        player.Animator.SetBool(animParmName, true);
        Debug.Log($"State: {animParmName}");
    }

    public virtual void Exit() {
        player.Animator.SetBool(animParmName , false);
    }

    public virtual void LogicUpdate() {

    }

    public virtual void PhysicsUpdate() {
        DoCheck();
    }

    public virtual void DoCheck() {

    }
}
