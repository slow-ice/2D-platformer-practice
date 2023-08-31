using Assets.Scripts.Refactoring.Architecture;
using Assets.Scripts.Refactoring.Controller.Player.FSM.Player_States.Sub_States;
using Assets.Scripts.Refactoring.Controller.Weapon;
using Assets.Scripts.Refactoring.Model.Player;
using Assets.Scripts.Refactoring.System.Battle_System;
using QFramework;
using System;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Refactoring {
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(SenseController))]
    public class PlayerController : MonoBehaviour, IController, IDamageable {

        public Animator mAnimator { get; private set; }
        public Rigidbody2D mRigidbody { get; private set; }

        public PlayerCore mCore;
        public PlayerStateMachine StateMachine = new PlayerStateMachine();
        private IOCContainer mStateDic = new IOCContainer();
        public PlayerData_SO PlayerData;
        public WeaponController weaponController;

        public Transform DashIndicator;

        [HideInInspector]
        public Vector2 CurrentVelocity;
        [HideInInspector]
        public Vector2 WorkSpace = new Vector2();

        public int amountOfJump = 1;

        public float DeathFadeTime = 0.5f;

        private void Awake() {
            InitializeComponent();
            InitializeFSM();
        }

        private void InitializeComponent() {
            mAnimator = GetComponent<Animator>();
            mRigidbody = GetComponent<Rigidbody2D>();

            mCore = new PlayerCore();
            mCore.InitCore(transform);

            weaponController = GetComponentInChildren<WeaponController>();

            this.GetModel<IPlayerModel>().RegisterPlayer(transform);
        }

        #region FSM

        private void InitializeFSM() {
            RegisterState(new PlayerIdleState("idle"));
            RegisterState(new PlayerMoveState("move"));
            RegisterState(new PlayerJumpState("inAir"));
            RegisterState(new PlayerLandState("land"));
            RegisterState(new PlayerInAirState("inAir"));
            RegisterState(new PlayerDashState("inAir"));
            RegisterState(new PlayerWallSlideState("wallSlide"));
            //RegisterState(new PlayerEdgeState("edgeClimbState"));
            RegisterState(new PlayerWallJumpState("inAir"));
            RegisterState(new PlayerAttackState("attack"));
            RegisterState(new PlayerHurtState("hurt"));
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
        #endregion

        void Start() {
            StateMachine.Initialize(GetState<PlayerIdleState>());
            
            GetState<PlayerAttackState>().SetWeaponController(weaponController);
        }


        private void Update() {
            if (Input.GetKeyDown(KeyCode.K)) {
                this.GetModel<IPlayerModel>().Health.Value -= 1;
            }

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
        public void SetVelocityToZero(float velo) {
            if (velo != 0) {
                return;
            }
            WorkSpace.Set(0f, 0f);
            mRigidbody.velocity = Vector2.zero;
            CurrentVelocity = Vector2.zero;
        }

        #endregion

        public Vector2 GetCornerPos() {
            var xhit = Physics2D.Raycast(mCore.Sense.wallCheckTrans.position, Vector2.right * mCore.FacingDirection,
                PlayerData.WallCheckDistance, LayerMask.GetMask("Ground"));
            float xdis = (xhit.distance + 0.015f) * mCore.FacingDirection;
            WorkSpace.Set(xdis, 0f);

            var yhit = Physics2D.Raycast(mCore.Sense.edgeCheckTrans.position + (Vector3)WorkSpace, Vector2.down,
                mCore.Sense.edgeCheckTrans.position.y - mCore.Sense.wallCheckTrans.position.y + 0.015f, LayerMask.GetMask("Ground"));
            float ydis = yhit.distance;

            WorkSpace.Set(transform.position.x + xdis, mCore.Sense.edgeCheckTrans.position.y - ydis);

            return WorkSpace;
        }

        public void Die() {
            mCore.Die(() => {
                StartCoroutine(deathFade(DeathFadeTime));
            });
        }

        IEnumerator deathFade(float fadeTime) {
            Color color = GetComponent<SpriteRenderer>().color;
            while (fadeTime > 0) {
                color.a -= fadeTime / Time.deltaTime;
                fadeTime -= Time.deltaTime;
                yield return null;
            }
            Destroy(gameObject);
        }

        public void Hurt(Action<IController> callback) {
            if (IsOnHurt())
                return;
            mCore.Hurt();
            callback?.Invoke(this);
        }

        public bool IsOnHurt() {
            return mCore.IsHurt;
        }

        public Transform getHitTransform() {
            return transform;
        }

        public IArchitecture GetArchitecture() {
            return GameCenter.Interface;
        }
    }

    public enum PlayerStatesEnum { 
        grounded, move, idle, land, ability, inAir, jump, dash, wallSlide, wallJump
    }
}