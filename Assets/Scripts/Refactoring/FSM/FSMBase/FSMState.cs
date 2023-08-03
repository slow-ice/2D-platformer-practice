using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Refactoring {
    public class FSMState<TState> : IFSMState<TState> {
        public TState stateType { get; set; }

        // 父状态
        public FSMState<TState> mParentState;

        public List<FSMTransition<TState>> mCurrentLayerTransitions;


        private Action<FSMState<TState>> mOnEnter;
        private Action<FSMState<TState>> mOnExit;
        private Action<FSMState<TState>> mOnUpdate;
        private Action<FSMState<TState>> mOnFixedUpdate;


        public FSMState(TState stateType) {
            this.stateType = stateType;
        }

        public FSMState() { }

        public virtual void OnInit() {

        }

        public virtual void OnInit(Action<FSMState<TState>> enter, Action<FSMState<TState>> update, Action<FSMState<TState>> exit) {
            mOnEnter = enter;
            mOnUpdate = update;
            mOnExit = exit;
        }

        /// <summary>
        /// 添加状态转换
        /// </summary>
        /// <param name="transition"></param>
        public virtual FSMTransition<TState> AddTransition(FSMTransition<TState> transition) {
            mCurrentLayerTransitions = mCurrentLayerTransitions ?? new List<FSMTransition<TState>>();
            mCurrentLayerTransitions.Add(transition);
            return transition;
        }

        public virtual void OnEnter() {
            mOnEnter?.Invoke(this);
        }

        public virtual void OnExit() {
            mOnExit?.Invoke(this);
        }

        public virtual void OnFixedUpdate() {
            mOnFixedUpdate?.Invoke(this);
        }

        public virtual void OnUpdate() {
            mOnUpdate?.Invoke(this);
        }
    }

    public class FSMState : FSMState<string> {
        public FSMState(string state) : base(state) { }
    }
}