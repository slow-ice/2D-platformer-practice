using QFramework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class PlayerState
{

    protected PlayerControler controler;
    protected PlayerStateMachine stateMachine;
    protected PlayerCore core;
    protected Player player;
    protected PlayerData_SO playerData;

    protected float startTime;
    protected bool forceTransition;

    protected string animParmName;

    public PlayerState(PlayerStateMachine stateMachine, Player player, PlayerData_SO playerData, string animParmName) {

    }

    public PlayerState(PlayerStateMachine playerStateMachine, PlayerControler playerControler,
        PlayerCore core, string animParmName = "null") {
        this.stateMachine = playerStateMachine;
        this.core = core;
        this.controler = playerControler;
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

    public virtual void OnUpdate() {

    }

    public virtual void OnFixedUpdate() {
        DoCheck();
    }

    public virtual void DoCheck() {

    }

    public PlayerState SetController(PlayerControler controler) {
        this.controler = controler;
        return this;
    }

    public PlayerState SetStateMachine(PlayerStateMachine playerStateMachine) {
        this.stateMachine = playerStateMachine;
        return this;
    }

    public PlayerState SetCore(PlayerCore core) {
        this.core = core;
        return this;
    }
}
