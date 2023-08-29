

using Assets.Scripts.Refactoring.Controller.Enemy.Base;
using Assets.Scripts.Refactoring.Event;
using Assets.Scripts.Refactoring.Model.Enemy;
using Assets.Scripts.Refactoring.Model.Player;
using QFramework;
using UnityEngine;

namespace Assets.Scripts.Refactoring.System.Battle_System {
    public interface IEnemyBattleSystem : ISystem { }

    public class EnemyBattleSystem : AbstractSystem, IEnemyBattleSystem {
        protected override void OnInit() {
            // EnemyHurt
            this.RegisterEvent<EnemyHurtEvent>(e => {
                var model = this.GetModel<IEnemyModel>();
                if (model.EnemyDic.TryGetValue(e.enmeyTrans, out var data)) {
                    if (data == null) {
                        Debug.LogWarning("data is null");
                        return;
                    }

                    if (e.IsHeavyHit) {
                        e.enmeyTrans.GetComponent<EnemyController>().HeavyHurt();
                    }
                    data.Health.Value -= e.damage;
                }
            });

            this.RegisterEvent<EnemyDieEvent>(e => {
                var controller = e.transform.GetComponent<EnemyController>();
                this.GetModel<IEnemyModel>().RemoveEnemy(e.transform);
                controller.Die();
            });
        }
    }
}
