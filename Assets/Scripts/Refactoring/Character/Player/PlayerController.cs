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
        private Animator mAnimator;
        private Rigidbody2D mRigidbody;
        private PlayerCore mCore;

        private void Awake() {
            InitializeFSM();
            InitializeComponent();
        }

        private void InitializeComponent() {
            mAnimator = GetComponent<Animator>();
            mRigidbody = GetComponent<Rigidbody2D>();
            mCore = new PlayerCore();
            mCore.InitCore(transform);
        }

        private void InitializeFSM() {
            PlayerStateMachine = new PlayerFSM();
            RegisterStateMachine(new PlayerFSM(), PlayerStatesEnum.grounded);
            RegisterState(new PlayerIdleState(PlayerStatesEnum.idle, "idle"),
                GetParent(PlayerStatesEnum.grounded));
        }

        public PlayerFSM GetParent(PlayerStatesEnum type) => (PlayerFSM)PlayerStateMachine.GetState(PlayerStatesEnum.grounded);

        private void RegisterState(PlayerStates state, PlayerFSM parent) {
            state.SetController(this)
                .SetCore(mCore);
            parent.AddState(state.stateType, state);
        }

        private void RegisterStateMachine(PlayerFSM subMachine, PlayerStatesEnum type) {
            PlayerStateMachine.AddState(type, subMachine);
        }

        private void Start() {
            PlayerStateMachine.OnInit();
        }

        private void Update() {
            PlayerStateMachine.OnUpdate();
        }

        private void FixedUpdate() {
            PlayerStateMachine.OnFixedUpdate();
        }

        public IArchitecture GetArchitecture() {
            return PlatformerArc.Interface;
        }
    }

    public enum PlayerStatesEnum { 
        grounded, move, idle, ability, inAir
    }
}