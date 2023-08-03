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
