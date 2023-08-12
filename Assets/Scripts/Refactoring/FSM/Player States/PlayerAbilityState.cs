using Assets.Scripts.Refactoring.FSM.Player_States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Refactoring {
    public class PlayerAbilityState : PlayerFSM {

        private bool isAbilityDone;
        private bool isGrounded;


        public override void OnInit() {
            base.OnInit();

            RegisterTransition(PlayerStatesEnum.idle, () => isAbilityDone 
                                && isGrounded && controller.CurrentVelocity.y < 0.1f);
            RegisterTransition(PlayerStatesEnum.inAir, () => isAbilityDone
                                && (!isGrounded || controller.CurrentVelocity.y < 0.1f));
        }

        public override void OnEnter() {
            base.OnEnter();
            isAbilityDone = false;
        }

        public override void OnUpdate() {
            base.OnUpdate();


            //if (isAbilityDone) {
            //    if (isGrounded && player.CurrentVelocity.y < 0.1f) {
            //        stateMachine.ChangeState(player.IdleState);
            //    }
            //    else {
            //        stateMachine.ChangeState(player.InAirState);
            //    }
            //}
        }

        public override void OnFixedUpdate() {
            base.OnFixedUpdate();
            isGrounded = core.CheckIfGrounded();
        }
    }
}