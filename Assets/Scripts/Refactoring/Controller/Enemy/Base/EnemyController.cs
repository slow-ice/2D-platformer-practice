using Assets.Scripts.Refactoring.Architecture;
using Assets.Scripts.Refactoring.Command;
using Assets.Scripts.Refactoring.Controller.Enemy.Base.Core;
using Assets.Scripts.Refactoring.Controller.Enemy.Base.FSM;
using Assets.Scripts.Refactoring.Controller.Enemy.FSM;
using Assets.Scripts.Refactoring.Event;
using Assets.Scripts.Refactoring.Model.Enemy;
using Assets.Scripts.Refactoring.System.Battle_System;
using QFramework;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Refactoring.Controller.Enemy.Base {
    public abstract class EnemyController : MonoBehaviour, IController, IDamageable {

        protected Animator mAnimator;
        protected Rigidbody2D mRigidbody;
        public BoxCollider2D HitBox;

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

            HitBox = transform.Find("HitBox").GetComponent<BoxCollider2D>();
            HitBox.enabled = false;

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

        public void Die() {
            mCore.Die();
        }

        public void HeavyHurt() {
            mCore.Hurt();
        }

        public void DestroySelf() {
            if (gameObject != null)
            Destroy(gameObject);
        }

        public IArchitecture GetArchitecture() {
            return GameCenter.Interface;
        }

        // 弃用
        public void DetectHitSuccess() {
            var boxPos = new Vector2(transform.position.x + HitBox.offset.x * transform.localScale.x,
                transform.position.y + HitBox.offset.y);
            var collision = Physics2D.OverlapBox(boxPos, HitBox.size, 0, LayerMask.GetMask("Player"));
            var ld = new Vector2(boxPos.x - HitBox.size.x, boxPos.y - HitBox.size.y);
            var ru = new Vector2(boxPos.x + HitBox.size.x, boxPos.y + HitBox.size.y);
            Debug.DrawLine(ld, ru, Color.red);
            if (collision != null) {

                IDamageable attackTarget = collision.GetComponent<IDamageable>();
                if (attackTarget == null) {
                    attackTarget = collision.GetComponentInParent<IDamageable>();
                }
                attackTarget?.Hurt(controller => {
                });
            }
        }

        public void Hurt(Action<IController> callback) {
            this.SendCommand(new AttackEnemyCommand(transform));
            callback?.Invoke(this);
        }

        public Transform getHitTransform() {
            return transform;
        }
    }
}