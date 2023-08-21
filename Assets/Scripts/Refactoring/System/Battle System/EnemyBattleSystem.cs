

using Assets.Scripts.Refactoring.Event;
using Assets.Scripts.Refactoring.Model.Enemy;
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
                    }
                    else {
                        data.Health.Value -= 1;
                    }
                }
            });
        }
    }
}
