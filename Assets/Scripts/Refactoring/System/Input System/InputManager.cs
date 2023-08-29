using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Refactoring.System.Input_System {
    public class InputManager : Singleton<InputManager> {
        public InputControl inputActions;

        public PlayerController player;

        public int xInput {
            get {
                return (int)(movementInput * Vector2.right).normalized.x;
            }
            private set { }
        }
        public int yInput {
            get {
                return (int)(movementInput * Vector2.up).normalized.y;
            }
            private set { }
        }
        public Vector2 movementInput { get; private set; }


        private bool jumpInput;
        public bool JumpInput {
            get {
                if (Time.time > jumpPressedTime + jumpInputBuffer) {
                    jumpInput = false;
                }
                return jumpInput;
            }
            private set { }
        }
        public bool jumpButtonUp { get; private set; }
        private float jumpPressedTime;
        public float jumpInputBuffer = 0.2f;
        public float jumpHoldTime;


        // Dash Params
        public bool DashInput { get; private set; }
        public bool DashButtonUp { get; private set; }
        public Vector2 DashDirectionInput { get; private set; }
        private float dashPressedTime;


        public bool[] AttackInputs;
        public bool Attack { get; set; }


        private void Start() {
            player = GameObject.Find("Player").GetComponent<PlayerController>();
        }

        private void OnEnable() {
            if (inputActions == null) {
                inputActions = new InputControl();
            }

            inputActions.Player.Move.performed += OnMoveInput;
            inputActions.Player.Move.canceled += ResetMoveInput;

            inputActions.Player.Jump.started += OnJumpInput;
            inputActions.Player.Jump.canceled += OnJumpButtonUp;

            inputActions.Player.Dash.performed += OnDashInput;
            inputActions.Player.Dash.canceled += OnDashButtonUp;
            inputActions.Player.DashDirection.performed += OnDirectionInput;

            inputActions.Player.PrimaryAttack.started += OnPrimaryAttackInput;
            inputActions.Player.PrimaryAttack.canceled += ResetPrimaryAttack;
            inputActions.Player.SecondAttack.started += OnSecondAttackInput;
            inputActions.Player.SecondAttack.canceled += ResetSecondAttack;

            inputActions.Enable();
        }

        public void DisableInput() {
            inputActions.Disable();
        }

        private void ResetSecondAttack(UnityEngine.InputSystem.InputAction.CallbackContext obj) =>
            AttackInputs[(int)CombatInputs.Secondary] = false;

        private void OnSecondAttackInput(UnityEngine.InputSystem.InputAction.CallbackContext obj) =>
            AttackInputs[(int)CombatInputs.Secondary] = true;


        private void ResetPrimaryAttack(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
            Attack = false;
            AttackInputs[(int)CombatInputs.Primary] = false;
        }

        private void OnPrimaryAttackInput(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
            Attack = true;
            AttackInputs[(int)CombatInputs.Primary] = true;
        }


        private void OnDashButtonUp(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
            DashInput = false;
            DashButtonUp = true;
        }

        private void OnDirectionInput(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
            DashDirectionInput = obj.ReadValue<Vector2>();
            DashDirectionInput = Camera.main.ScreenToWorldPoint((Vector3)DashDirectionInput) - player.transform.position;
            DashDirectionInput.Normalize();
            DashButtonUp = false;
        }

        private void OnDashInput(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
            DashInput = true;
            dashPressedTime = Time.time;
        }

        public void UseDashInput() => DashInput = false;

        private void OnJumpButtonUp(UnityEngine.InputSystem.InputAction.CallbackContext obj) =>
            jumpButtonUp = true;

        private void ResetMoveInput(UnityEngine.InputSystem.InputAction.CallbackContext obj) =>
            movementInput = Vector2.zero;

        private void OnMoveInput(UnityEngine.InputSystem.InputAction.CallbackContext obj) =>
            movementInput = obj.ReadValue<Vector2>();

        public void UseJumpInput() => jumpInput = false;

        private void OnJumpInput(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
            jumpInput = true;
            jumpButtonUp = false;
            jumpPressedTime = Time.time;
        }
    }
}

public enum CombatInputs {
    Primary,
    Secondary,
}