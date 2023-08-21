using Assets.Scripts.Refactoring.Controller.Enemy.Base;
using Assets.Scripts.Refactoring.Event;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Refactoring.Model.Enemy {
    public interface IEnemyModel : IModel {
        Dictionary<Transform, EnemyRunTimeData> EnemyDic { get; }
    }

    public class EnemyModel : AbstractModel, IEnemyModel {

        public Dictionary<Transform, EnemyRunTimeData> EnemyDic { get; } = new();

        protected override void OnInit() {
            RegisterAllEnemy();
            
        }

        void RegisterAllEnemy() {
            var enemys = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (var enemy in enemys) {
                var data = new EnemyRunTimeData(enemy.transform);
                Debug.Log("Enemy " + enemy.name + " registered");

                data.Health.Register(onValueChanged => {
                    Debug.Log("Blue pig Current Health: " + onValueChanged);

                    if (onValueChanged <= 0) {
                        this.SendEvent(new EnemyDieEvent(enemy.transform));
                    }
                }).UnRegisterWhenGameObjectDestroyed(enemy);

                EnemyDic.Add(enemy.transform, data);
            }
        }

        public EnemyRunTimeData GetEnemy(Transform target) {
            if (EnemyDic.ContainsKey(target)) {
                return EnemyDic[target];
            }
            Debug.LogWarning("enemy transform not registered");
            return null;
        }
    }
}