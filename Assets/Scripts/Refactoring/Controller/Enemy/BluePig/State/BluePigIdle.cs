
using Assets.Scripts.Refactoring.Controller.Enemy.BluePig;
using Assets.Scripts.Refactoring.Controller.Enemy.BluePig.State;
using UnityEngine;

namespace Assets.Scripts.Refactoring.Controller.Enemy {
    public class BluePigIdle : BluePigBaseState {

        private float stayTime;

        public BluePigIdle(string animName) : base(animName) {
            stayTime = 0.5f;
        }

        public override void OnEnter() {
            base.OnEnter();

            core.PlayAnim(Base.Core.EnemyAnimType.Idle);
            core.SetVelocity(0f);
        }

        public override void OnUpdate() {
            base.OnUpdate();

            if (core.DetectPlayer()) {
                ChangeState<BluePigChase>();
                return;
            }

            if (stayTime > 0) {
                stayTime -= Time.deltaTime;
            }
            else {
                ChangeState<BluePigPatrolState>();
            }
        }

        public void SetStayTime(float stayTime) {
            this.stayTime = stayTime;
        }
    }
}
