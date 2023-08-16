using Assets.Scripts.Refactoring;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    protected PlayerController controller;
    protected PlayerCore core;
    protected PlayerData_SO playerData;

    protected float startTime;

    protected string animParmName;

    public PlayerState(PlayerStateMachine stateMachine, Player player, PlayerData_SO playerData, string animParmName) {
        this.stateMachine = stateMachine;
        this.player = player;
        this.playerData = playerData;
        this.animParmName = animParmName;
    }

    public PlayerState(string animParmName) {
        this.animParmName = animParmName;
    }

    public virtual void OnEnter() {
        DoChecks();
        startTime = Time.time;
        core.mAnimator.SetBool(animParmName, true);
        Debug.Log($"State: {animParmName}");
    }

    public virtual void OnExit() {
        core.mAnimator.SetBool(animParmName , false);
    }

    public virtual void OnUpdate() {

    }

    public virtual void OnFixedUpdate() {
        DoChecks();
    }

    public virtual void DoChecks() {

    }

    public PlayerState SetCore(PlayerCore core) {
        this.core = core;
        return this;
    }

    public PlayerState SetController(PlayerController controller) {
        this.controller = controller;
        return this;
    }

    public PlayerState SetStateMachine(PlayerStateMachine stateMachine) {
        this.stateMachine = stateMachine;
        return this;
    }
}
