

using UnityEngine;

namespace Assets.Scripts.Refactoring.Controller.Player.FSM.Player_States.Sub_States {
    internal class PlayerHurtState : PlayerState {
        private bool isHurtOver = false;

        public PlayerHurtState(string name) : base(name) { }

        public override void OnEnter() {
            base.OnEnter();

            isHurtOver = false;

            controller.SetVelocity(core.PlayerData.hurtForce,
                new Vector2(1, 1), core.HurtDirection);

            WaitTimeManager.WaitTime(controller.PlayerData.hurtTime, () => {
                isHurtOver = true;
                core.IsHurt = false;
            });
        }

        public override void OnUpdate() {
            base.OnUpdate();

            if (core.IsCurrentAnimOver() && isHurtOver) {
                stateMachine.ChangeState(controller.GetState<PlayerIdleState>());
            }
        }

    }
}
