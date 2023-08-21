using Assets.Scripts.Refactoring.Event;
using QFramework;
using UnityEngine;

namespace Assets.Scripts.Refactoring.Command {
    public class TryAttackCommand : AbstractCommand {
        private Transform targetTrans;
        private int damage;

        public TryAttackCommand(Transform transform, int damage) {
            this.targetTrans = transform;
            this.damage = damage;
        }
        protected override void OnExecute() {
            this.SendEvent(new EnemyHurtEvent(targetTrans, damage));
        }
    }
}
