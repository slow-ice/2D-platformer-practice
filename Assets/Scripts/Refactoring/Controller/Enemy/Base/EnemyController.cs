using Assets.Scripts.Refactoring.Architecture;
using Assets.Scripts.Refactoring.Controller.Enemy.Base.Core;
using Assets.Scripts.Refactoring.Controller.Enemy.Base.FSM;
using Assets.Scripts.Refactoring.Controller.Enemy.FSM;
using Assets.Scripts.Refactoring.Model.Enemy;
using QFramework;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Refactoring.Controller.Enemy.Base {
    public abstract class EnemyController : MonoBehaviour, IController {

        protected Animator mAnimator;
        protected Rigidbody2D mRigidbody;

        protected EnemyCore mCore;
        public EnemyData_SO mEnemyData;
        protected EnemyStateMachine mStateMachine = new EnemyStateMachine();
        private IOCContainer mStateDic = new IOCContainer();

        // 指定子类相应起始状态
        protected abstract EnemyState InitialState { get; }
        // 指定子类相应Core, 在init component 中注册
        protected abstract EnemyCore InitialCore { get; }

        public void OnInit() {
            InitializeComponent();
            InitializeFSM();
            mStateMachine.OnInit(InitialState);
        }

        public void Awake() {
            OnInit();
        }

        protected virtual void Update() {
            mStateMachine.OnUpdate();
        }

        void FixedUpdate() {
            mStateMachine.OnFixedUpdate();
        }

        public virtual void InitializeComponent() {
            mAnimator = GetComponent<Animator>();
            mRigidbody = GetComponent<Rigidbody2D>();
            RegisterCore(InitialCore);
        }

        private void RegisterCore(EnemyCore enemyCore) {
            mCore = enemyCore;
            mCore.SetController(this)
                .SetAnimator(mAnimator)
                .SetRigibody(mRigidbody)
                .SetData(mEnemyData)
                .OnInit();
        }

        /// <summary>
        /// 注册状态
        /// </summary>
        protected abstract void InitializeFSM();

        protected void RegisterState<Tstate>(Tstate state) where Tstate : EnemyState {
            state.OnInit(mStateMachine, this, mCore);

            mStateDic.Register<Tstate>(state);
        }

        public TState GetState<TState>() where TState : EnemyState {
            return mStateDic.Get<TState>();
        }

        public IArchitecture GetArchitecture() {
            return GameCenter.Interface;
        }
    }
}