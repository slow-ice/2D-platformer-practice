

using Assets.Scripts.Refactoring.Controller.Enemy.Base;
using Assets.Scripts.Refactoring.Event;
using QFramework;
using UnityEngine;

namespace Assets.Scripts.Refactoring.Command {
    public class AttackPlayerCommand : AbstractCommand {
        public Transform EnemyTrans;

        public AttackPlayerCommand(Transform transform) {
            EnemyTrans = transform;
        }

        protected override void OnExecute() {
            Debug.Log("Execute attack command");
            var data = EnemyTrans.GetComponent<EnemyController>().mEnemyData;
            this.SendEvent(new PlayerHurtEvent(data.normalAttackDamage));
        }
    }
}
