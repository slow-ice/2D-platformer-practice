

using Assets.Scripts.Refactoring.Controller.Enemy.Base;
using QFramework;
using UnityEngine;

namespace Assets.Scripts.Refactoring.Model.Enemy {
    public class EnemyRunTimeData {
        public Transform Transform { get; }
        public Animator Animator { get; }
        public Rigidbody2D Rigidbody { get; }
        public EnemyController Controller { get; }
        public BindableProperty<int> Health { get; set; } = new BindableProperty<int>();

        public EnemyRunTimeData(Transform transform) {
            Transform = transform;
            Animator = transform.GetComponent<Animator>();
            Rigidbody = transform.GetComponent<Rigidbody2D>();
            Controller = transform.GetComponent<EnemyController>();
            Health.Value = Controller.mEnemyData.maxHealthPoint;
        }
    }
}
