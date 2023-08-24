using Assets.Scripts.Refactoring.Controller.Enemy.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Refactoring.Event {
    public class PlayerHeavyAttackEvent {
        public PlayerController Controller { get; }
        public EnemyController EnemyController { get; }

        public PlayerHeavyAttackEvent(PlayerController controller, EnemyController enemyController) {
            Controller = controller;
            EnemyController = enemyController;
        }
    }
}
