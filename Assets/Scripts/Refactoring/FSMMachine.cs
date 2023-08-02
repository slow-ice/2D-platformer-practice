using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Refactoring {
    public class FSMMachine<TState> : FSMState<TState>, IFSMMachine<TState> {
        public FSMState<TState> ActiveSubState { get; set; }
        public FSMState<TState> DefaultSubState { get; set; }

        // 所有状态
        private Dictionary<TState, FSMState<TState>> mAllSubStates =
            new Dictionary<TState, FSMState<TState>>();

        private List<FSMTransition<TState>> mSubLayerTransitions;
        private List<FSMTransition<TState>> mAnyTransitions = new List<FSMTransition<TState>>();

        private static readonly List<FSMTransition<TState>> noTransitions = new List<FSMTransition<TState>>(0);

        public bool IsRootMachine { get { return mParentState == null; } set { mParentState = null; }  }



        public override void OnInit() {
            if (!IsRootMachine) {
                return;
            }

            OnEnter();
        }

        /// <summary>
        /// 添加状态跳转
        /// </summary>
        /// <param name="transition"></param>
        public override void AddTransition(FSMTransition<TState> transition) {
            if (mAllSubStates.TryGetValue(transition.FromState, out var state)) {
                state.AddTransition(transition);
                base.AddTransition(transition);
            }
            else {
                base.AddTransition(transition);
            }
        }

        /// <summary>
        /// 从当前状态中的子状态, 跳转到任意状态
        /// </summary>
        /// <param name="transition">任意状态皆可</param>
        public void AddAnyTransition(FSMTransition<TState> transition) {
            mParentState.AddTransition(transition);
        }

        public bool TryTransition(FSMTransition<TState> transition) {
            if (transition.Equals(stateType, transition.ToState)
                || !transition.Tick()) {
                return false;
            }

            ChangeState(transition.ToState);

            return true;
        }

        /// <summary>
        /// 每帧调用, 检查大状态之间的转换, 和当前子状态之间的转换
        /// </summary>     
        private void CheckTransition() {
            // 在激活状态的转换列表中寻找第一个合法的转换
            mSubLayerTransitions.First(trans => TryTransition(trans));
        }


        public FSMState<TState> GetState(TState state) {
            if (mAllSubStates.TryGetValue(state, out FSMState<TState> newState)) {
                return newState;
            }
            Debug.LogError("No such state in this state machine.");
            return null;
        }

        public void AddState(TState type, FSMState<TState> state) {
            state.stateType = type;
            state.mParentState = this;
            state.OnInit();

            if (mAllSubStates.Count == 0) {
                DefaultSubState = state;
            }

            mAllSubStates.Add(type, state);
        }

        public void ChangeState(TState state) {
            ActiveSubState?.OnExit();
            
            var newState = GetState(state);
            mSubLayerTransitions = newState.mCurrentLayerTransitions ?? noTransitions;
            ActiveSubState = newState;

            Debug.Log("Transition to active sub state: " + ActiveSubState.stateType);

            ActiveSubState.OnEnter();
        }

        public void SetDefaultState(FSMState<TState> state) => DefaultSubState = state;

        public override void OnEnter() {
            base.OnEnter();

            if (DefaultSubState == null) {
                Debug.LogError("No default state.");
            }

            ChangeState(DefaultSubState.stateType);
        }

        public override void OnUpdate() {
            base.OnUpdate();

            CheckTransition();

            ActiveSubState.OnUpdate();
            Debug.Log("Current active state: " + this.stateType);
        }

        public override void OnFixedUpdate() {
            base.OnFixedUpdate();

            ActiveSubState.OnFixedUpdate();
        }

        public override void OnExit() {
            base.OnExit();

            ActiveSubState?.OnExit();
        }
    }


}