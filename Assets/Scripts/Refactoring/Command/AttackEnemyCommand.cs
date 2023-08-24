using Assets.Scripts.Refactoring.Event;
using Assets.Scripts.Refactoring.Model.Player;
using QFramework;
using UnityEngine;

namespace Assets.Scripts.Refactoring.Command {
    public class AttackEnemyCommand : AbstractCommand {
        private Transform targetEnemyTrans;

        public AttackEnemyCommand(Transform transform) {
            this.targetEnemyTrans = transform;
        }
        protected override void OnExecute() {
            //this.SendEvent(new EnemyHurtEvent(targetTrans, damage, IsHeavyHit));
            var controller = this.GetModel<IPlayerModel>().Controller;
            var weaponData = controller.weaponController.CurrentWeapon.weaponData;
            var state = controller.StateMachine.CurrentState as PlayerAttackState;
            if (state.IsLastHit()) {
                this.SendEvent(new EnemyHurtEvent(this.targetEnemyTrans, weaponData.HeavyHitDamage, true));
            }
            else {
                this.SendEvent(new EnemyHurtEvent(this.targetEnemyTrans, weaponData.NormalDamage, false));
            }
        }
    }
}
