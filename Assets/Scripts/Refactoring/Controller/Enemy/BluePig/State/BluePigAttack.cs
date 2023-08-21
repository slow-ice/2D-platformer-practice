using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Refactoring.Controller.Enemy.BluePig.State {
    public class BluePigAttack : BluePigBaseState {
        public BluePigAttack(string animName) : base(animName) {
        }

        public override void OnEnter() {
            base.OnEnter();

            core.PlayAnim(Base.Core.EnemyAnimType.Attack);
        }

        public override void OnUpdate() {
            base.OnUpdate();

            if (core.IsAnimationOver()) {
                ChangeState<BluePigPatrolState>();
                return;
            }
        }
    }
}