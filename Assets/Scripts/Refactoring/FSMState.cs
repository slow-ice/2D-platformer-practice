using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Refactoring {
    public class FSMState<TState> : IFSMState<TState> {
        public TState stateType { get; set; }

        // 父状态
        public FSMState<TState> mParentState;


        private Action<FSMState<TState>> mOnEnter;
        private Action<FSMState<TState>> mOnExit;
        private Action<FSMState<TState>> mOnUpdate;
        private Action<FSMState<TState>> mOnFixedUpdate;

        public virtual void OnInit() {

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
}