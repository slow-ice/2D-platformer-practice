using Assets.Scripts.Refactoring.Utilities;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Refactoring.Controller.Enemy.BluePig.State {
    public class BluePigAttack : BluePigBaseState {
        public BluePigAttack(string animName) : base(animName) {
        }

        public override void OnEnter() {
            base.OnEnter();

            core.PlayAnim(Base.Core.EnemyAnimType.Attack);

            core.CheckShouldFlip(core.mPlayerTrans);
        }

        public override void OnUpdate() {
            base.OnUpdate();

            Debug.DrawRay(controller.transform.realPosition(), 
                new Vector2(controller.transform.localScale.x * EnemyData.attackRange, 0), Color.blue);

            if (core.IsAnimationOver()) {
                ChangeState<BluePigChase>();
                return;
            }
        }

        public override void OnFixedUpdate() {
            base.OnFixedUpdate();

            core.AttackPlayer();
        }

        public override void OnExit() {
            base.OnExit();

            core.lastAttackTime = Time.time;
        }
    }
}