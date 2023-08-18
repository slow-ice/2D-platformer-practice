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
        [SerializeField]
        protected EnemyData_SO mEnemyData;
        protected EnemyStateMachine mStateMachine = new EnemyStateMachine();
        private IOCContainer mStateDic = new IOCContainer();

        protected abstract EnemyState InitialState { get; }

        public void OnInit() {
            InitializeComponent();
            InitializeFSM();
            mStateMachine.OnInit(InitialState);
        }

        public void Awake() {
            OnInit();
        }

        void Update() {
            mStateMachine.OnUpdate();
        }

        void FixedUpdate() {
            mStateMachine.OnFixedUpdate();
        }

        protected void InitializeComponent() {
            mAnimator = GetComponent<Animator>();
            mRigidbody = GetComponent<Rigidbody2D>();
            mCore = new EnemyCore();
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