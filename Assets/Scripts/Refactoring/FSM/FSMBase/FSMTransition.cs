using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace Assets.Scripts.Refactoring {
    public class FSMTransition<TState> {
        public TState FromState {  get; set; }

        public TState ToState { get; set; }

        public List<Func<bool>> mConditions = new List<Func<bool>>();

        public FSMTransition(TState fromState, TState toState) { 
            this.FromState = fromState; 
            this.ToState = toState; 
        }

        /// <summary>
        /// 传递条件的回调函数
        /// </summary>
        /// <param name="condition">条件回调函数</param>
        public void AddCondition(Func<bool> condition) {
            if (mConditions.Contains(condition)) {
                return;
            }

            mConditions.Add(condition);
        }

        /// <summary>
        /// 判断所有转换条件都成立
        /// </summary>
        /// <returns></returns>
        public bool Tick() {
            bool allConditionsMet = mConditions.All(condition => condition());
            return allConditionsMet;
        }

        /// <summary>
        /// 判断相同, 使用TState类型的默认比较器
        /// </summary>
        /// <param name="fromState"></param>
        /// <param name="toState"></param>
        /// <returns></returns>
        public bool Equals(TState fromState, TState toState) {
            return EqualityComparer<TState>.Default.Equals(fromState, toState);
        }
    }

    public class FSMTransition : FSMTransition<string> {
        public FSMTransition(string fromState, string toState) : base(fromState, toState) { }
    }
}