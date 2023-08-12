using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Refactoring.FSM.Player_States {
    public class PlayerTransition : FSMTransition<PlayerStatesEnum> {
        public PlayerTransition(PlayerStatesEnum fromState, PlayerStatesEnum toState) : base(fromState, toState) {
        }

        /// <summary>
        /// 一个条件的简单转换
        /// </summary>
        /// <param name="fromState"></param>
        /// <param name="toState"></param>
        /// <param name="func">简单的条件集合</param>
        public PlayerTransition(PlayerStatesEnum fromState, PlayerStatesEnum toState, Func<bool> func)
            : base(fromState, toState) {
            this.FromState = fromState;
            this.ToState = toState;
            mConditions.Add(func);
        }
    }
}