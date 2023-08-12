using Assets.Scripts.Refactoring.Architecture;
using Assets.Scripts.Refactoring.FSM.Player_States;
using QFramework;
using System;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace Assets.Scripts.Refactoring {
    public class PlayerController : MonoBehaviour, IController {

        public PlayerFSM PlayerStateMachine { get; private set; }
        public Animator mAnimator { get; private set; }
        public Rigidbody2D mRigidbody { get; private set; }
        private PlayerCore mCore;
        public PlayerData_SO PlayerData;

        public Vector2 CurrentVelocity;
        public Vector2 WorkSpace = new Vector2();

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
            PlayerStateMachine = new PlayerFSM();
            PlayerStateMachine.IsRootMachine = true;

            RegisterStateMachine(PlayerStatesEnum.grounded);

            RegisterState(new PlayerIdleState(PlayerStatesEnum.idle, "idle"),
                GetParent(PlayerStatesEnum.grounded));
            RegisterState(new PlayerMoveState(PlayerStatesEnum.move, "move"),
                GetParent(PlayerStatesEnum.grounded));
            RegisterState(new PlayerJumpState(PlayerStatesEnum.jump, "inAir"),
                PlayerStateMachine);
        }

        public PlayerFSM GetParent(PlayerStatesEnum type) => (PlayerFSM)PlayerStateMachine.GetState(type);

        private void RegisterState(PlayerStates state, PlayerFSM parent) {
            state.SetController(this)
                .SetCore(mCore);
            parent.AddState(state.stateType, state);
        }

        private void RegisterStateMachine(PlayerStatesEnum type) {
            PlayerFSM subMachine = new PlayerFSM();
            PlayerStateMachine.AddState(type, subMachine);
        }

        private void Start() {
            PlayerStateMachine.OnInit();
        }

        private void Update() {
            PlayerStateMachine.OnUpdate();

            CurrentVelocity = mRigidbody.velocity;
        }

        private void FixedUpdate() {
            PlayerStateMachine.OnFixedUpdate();
        }

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

        public IArchitecture GetArchitecture() {
            return PlatformerArc.Interface;
        }
    }

    public enum PlayerStatesEnum { 
        grounded, move, idle, land, ability, inAir, jump, dash, wallSlide, wallJump
    }
}