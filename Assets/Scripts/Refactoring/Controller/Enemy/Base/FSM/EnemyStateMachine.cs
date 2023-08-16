using Assets.Scripts.Refactoring.Controller.Enemy.Base.FSM;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Refactoring.Controller.Enemy.FSM {
    public class EnemyStateMachine {

        public EnemyState CurrentState { get; private set; }

        public void OnInit(EnemyState state) {
            CurrentState = state;
            CurrentState.OnEnter();
        }

        public void ChangeState(EnemyState state) {
            CurrentState?.OnExit();
            CurrentState = state;
            CurrentState.OnEnter();
        }

        public void OnUpdate() {
            CurrentState.OnUpdate();
        }

        public void OnFixedUpdate() {
            CurrentState?.OnFixedUpdate();
        }
    }
}