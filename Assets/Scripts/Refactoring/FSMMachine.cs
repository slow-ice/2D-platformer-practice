using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Refactoring {
    public class FSMMachine<TState> : FSMState<TState>, IFSMMachine<TState> {
        public FSMState<TState> LastState { get; set; }
        public FSMState<TState> CurrentState { get; set; }
        public FSMState<TState> DefaultState { get; set; }

        // 所有状态
        private Dictionary<TState, FSMState<TState>> mAllStates =
            new Dictionary<TState, FSMState<TState>>();

        public bool IsRootMachine { get { return mParentState == null; } }



        public override void OnInit() {
        }


        public void TryTransition(TState stateName) {
        }

        public void AddState(TState type, FSMState<TState> state) {
            state.stateType = type;
            state.mParentState = this;
            state.OnInit();

            if (mAllStates.Count == 0) {
                DefaultState = state;
            }

            mAllStates.Add(type, state);
        }

        public void ChangeState(TState state) {
            if (CurrentState != null) {
                CurrentState.OnExit();
            }

            LastState = CurrentState;
            if (!mAllStates.TryGetValue(state, out FSMState<TState> newState)) {
                Debug.LogError("No such state in this state machine.");
            }
            CurrentState = newState;

            CurrentState.OnEnter();
        }

        public void SetDefaultState(FSMState<TState> state) => DefaultState = state;

        public override void OnEnter() {
            base.OnEnter();

            if (DefaultState == null) {
                Debug.LogError("No default state.");
            }
            
        }

        public override void OnExit() {
        }

        public override void OnFixedUpdate() {
        }


        public override void OnUpdate() {
        }
    }
}