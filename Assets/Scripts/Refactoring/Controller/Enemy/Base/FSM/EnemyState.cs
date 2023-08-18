using Assets.Scripts.Refactoring.Controller.Enemy.Base.Core;
using Assets.Scripts.Refactoring.Controller.Enemy.FSM;
using Assets.Scripts.Refactoring.Model.Enemy;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Refactoring.Controller.Enemy.Base.FSM {
    public class EnemyState {
        private EnemyStateMachine mStateMachine;
        protected EnemyController controller;
        protected EnemyData_SO EnemyData;
        protected EnemyCore core;

        protected string animParaName;
        protected float startTime;
        protected bool isStateOver = false;

        public EnemyState(string animName) {
            animParaName = animName;
        }

        public EnemyState OnInit(EnemyStateMachine stateMachine, EnemyController enemyController, EnemyCore enemyCore) {
            mStateMachine = stateMachine;
            controller = enemyController;
            core = enemyCore;
            EnemyData = core.mEnemyData;
            return this;
        }

        public virtual void OnEnter() {

        }

        public virtual void OnUpdate() {

        }

        public virtual void OnFixedUpdate() {

        }

        public virtual void OnExit() {

        }

        protected void ChangeState<TState>() where TState : EnemyState {
            if (isStateOver) {
                return;
            }

            mStateMachine.ChangeState(GetState<TState>());
        }

        protected TState GetState<TState>() where TState : EnemyState {
            return controller.GetState<TState>();
        }
    }
}
