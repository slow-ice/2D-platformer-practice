using Assets.Scripts.Refactoring.FSM.Player_States;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Refactoring {
    public class PlayerStates : FSMState<PlayerStatesEnum> {

        protected PlayerCore core;
        protected PlayerController controller;

        protected string animParaName;

        protected float startTime;

        public PlayerStates(PlayerStatesEnum stateType, string animName) {
            this.stateType = stateType;
            animParaName = animName;
        }

        /// <summary>
        /// 所有转换写在这里
        /// </summary>
        public override void OnInit() {
            base.OnInit();
        }

        public override void OnEnter() {
            base.OnEnter();
            Debug.Log("Enter  " +  this.stateType.ToString());
            startTime = Time.time;
        }

        public virtual void PhysicalChecks() {

        }

        public override void OnFixedUpdate() {
            base.OnFixedUpdate();
            PhysicalChecks();
        }

        public PlayerTransition RegisterTransition(PlayerStatesEnum toState) {
            var transition = new PlayerTransition(this.stateType, toState);
            return transition;
        }

        /// <summary>
        /// 注册转化, 直接添加到transitions中
        /// </summary>
        /// <param name="toState"></param>
        /// <param name="cond"></param>
        /// <returns></returns>
        public PlayerTransition RegisterTransition(PlayerStatesEnum toState, Func<bool> cond) {
            var transition = new PlayerTransition(this.stateType, toState, cond);
            AddTransition(transition);
            return transition;
        }

        public PlayerStates SetCore(PlayerCore core) {
            this.core = core;
            return this;
        }

        public PlayerStates SetController(PlayerController controller) {
            this.controller = controller;
            return this;
        }
    }
}
