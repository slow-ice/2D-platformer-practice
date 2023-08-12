using Assets.Scripts.Refactoring.FSM.Player_States;
using Assets.Scripts.Refactoring.System.Input_System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Refactoring {
    public class PlayerGroundState : PlayerFSM {

        // #region Old
        protected int xInput;

        private bool jumpInput;
        private bool isGrounded;
        //protected bool changeToJump;
        //private bool dashInput;

        public PlayerGroundState(PlayerStatesEnum stateType) : base() {
            this.stateType = stateType;
        }

        //public override void Enter() {
        //    base.Enter();

        //    changeToJump = false;
        //    player.JumpState.ResetJumpLeft();
        //    player.DashState.ResetCanDash();
        //}

        ////public override void OnUpdate() {
        ////    base.OnUpdate();

        ////    xInput = player.InputHandler.xInput;
        ////    jumpInput = player.InputHandler.JumpInput;
        ////    dashInput = player.InputHandler.DashInput;

        ////    if (player.InputHandler.AttackInputs[(int)CombatInputs.Primary]) {
        ////        stateMachine.ChangeState(player.PrimaryAttackState);
        ////        return;
        ////    }

        ////    if (player.InputHandler.AttackInputs[(int)(CombatInputs.Secondary)]) {
        ////        stateMachine.ChangeState(player.SecondAttackState);
        ////        return;
        ////    }

        ////    if (jumpInput && player.JumpState.CanJump) {
        ////        changeToJump = true;
        ////        player.InputHandler.UseJumpInput();
        ////        stateMachine.ChangeState(player.JumpState);
        ////        return;
        ////    }

        ////    if (!isGrounded) {
        ////        player.InAirState.StartCoyoteTime();
        ////        stateMachine.ChangeState(player.InAirState);
        ////        return;
        ////    }

        ////    if (dashInput && player.DashState.CheckIfCanDash()) {
        ////        stateMachine.ChangeState(player.DashState);
        ////        return;
        ////    }
        ////}
        //#endregion


        public override void OnInit() {
            base.OnInit();
            RegisterTransition(PlayerStatesEnum.jump, () => {
                if (jumpInput && core.CanJump) {
                    InputManager.Instance.UseJumpInput();
                    return true;
                }
                return false;
            });
        }

        public override void OnUpdate() {
            base.OnUpdate();
            xInput = InputManager.Instance.xInput;
            jumpInput = InputManager.Instance.JumpInput;
            //dashInput = InputManager.Instance.DashInput;
        }
        public override void OnFixedUpdate() {
            base.OnFixedUpdate();
            isGrounded = core.CheckIfGrounded();
        }

        public override void OnEnter() {
            base.OnEnter();
            core.ResetJumpLeft();
        }
    }
}