using Assets.Scripts.Refactoring.Architecture;
using Assets.Scripts.Refactoring.Model.Player;
using QFramework;
using System;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace Assets.Scripts.Refactoring {
    public class PlayerController : MonoBehaviour, IController {

        public Animator mAnimator { get; private set; }
        public Rigidbody2D mRigidbody { get; private set; }

        public PlayerCore mCore;
        public PlayerStateMachine StateMachine = new PlayerStateMachine();
        private IOCContainer mStateDic = new IOCContainer();
        public PlayerData_SO PlayerData;

        public Transform DashIndicator;

        public Vector2 CurrentVelocity;
        public Vector2 WorkSpace = new Vector2();

        public int amountOfJump = 1;

        private void Awake() {
            InitializeComponent();
            InitializeFSM();
        }

        private void InitializeComponent() {
            mAnimator = GetComponent<Animator>();
            mRigidbody = GetComponent<Rigidbody2D>();

            mCore = new PlayerCore();
            mCore.InitCore(transform);
        }

        private void InitializeFSM() {
            RegisterState(new PlayerIdleState("idle"));
            RegisterState(new PlayerMoveState("move"));
            RegisterState(new PlayerJumpState("inAir"));
            RegisterState(new PlayerLandState("land"));
            RegisterState(new PlayerInAirState("inAir"));
            RegisterState(new PlayerDashState("inAir"));
            RegisterState(new PlayerWallSlideState("wallSlide"));
            RegisterState(new PlayerEdgeState("edgeClimbState"));
            RegisterState(new PlayerWallJumpState("inAir"));
            RegisterState(new PlayerAttackState("attack"));
        }


        private void RegisterState<TState>(TState state) where TState : PlayerState {
            state.SetController(this)
                .SetCore(mCore)
                .SetStateMachine(StateMachine);
            mStateDic.Register<TState>(state);
        }

        public TState GetState<TState>() where TState : PlayerState {
            return mStateDic.Get<TState>();
        }


        void Start() {
            StateMachine.Initialize(GetState<PlayerIdleState>());
        }


        private void Update() {
            CurrentVelocity = mRigidbody.velocity;

            StateMachine.CurrentState.OnUpdate();
        }

        private void FixedUpdate() {
            StateMachine.CurrentState.OnFixedUpdate();
        }

        #region Set Funcs
        public void SetVelocityX(float veloX) {
            WorkSpace.Set(veloX, CurrentVelocity.y);
            mRigidbody.velocity = WorkSpace;
            CurrentVelocity = WorkSpace;
        }

        public void SetVelocityY(float veloY) {
            WorkSpace.Set(CurrentVelocity.x, veloY);
            mRigidbody.velocity = WorkSpace;
            CurrentVelocity = WorkSpace;
        }

        public void SetVelocity(float velo, Vector2 angle, int direction) {
            angle.Normalize();
            WorkSpace.Set(velo * angle.x * direction, velo * angle.y);
            mRigidbody.velocity = WorkSpace;
            CurrentVelocity = WorkSpace;
        }

        /// <summary>
        /// Set velocity to zero
        /// </summary>
        /// <param name="velo"></param>
        public void SetVelocity(float velo) {
            if (velo != 0) {
                return;
            }
            WorkSpace.Set(0f, 0f);
            mRigidbody.velocity = Vector2.zero;
            CurrentVelocity = Vector2.zero;
        }

        #endregion

        public IArchitecture GetArchitecture() {
            return GameCenter.Interface;
        }
    }

    public enum PlayerStatesEnum { 
        grounded, move, idle, land, ability, inAir, jump, dash, wallSlide, wallJump
    }
}