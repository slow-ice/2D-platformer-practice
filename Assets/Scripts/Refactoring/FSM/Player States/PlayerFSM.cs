using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Refactoring.FSM.Player_States {
    public class PlayerFSM : FSMMachine<PlayerStatesEnum> {
        protected PlayerCore core;
        protected PlayerController controller;

        public PlayerFSM() : base() { }

        public PlayerFSM(PlayerStatesEnum stateType) : base() {
            this.stateType = stateType;
        }


        /// <summary>
        /// 注册转化, 从当前大状态到其他状态的转化
        /// </summary>
        /// <param name="toState"></param>
        /// <param name="cond"></param>
        /// <returns></returns>
        public PlayerTransition RegisterTransition(PlayerStatesEnum toState, Func<bool> cond) {
            var transition = new PlayerTransition(this.stateType, toState, cond);
            AddTransition(transition);
            return transition;
        }

        public PlayerFSM SetCore(PlayerCore core) {
            this.core = core;
            return this;
        }

        public PlayerFSM SetController(PlayerController controller) {
            this.controller = controller;
            return this;
        }
    }
}