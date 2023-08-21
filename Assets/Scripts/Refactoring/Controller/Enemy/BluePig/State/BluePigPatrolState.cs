
using Assets.Scripts.Refactoring.Controller.Enemy.Base.FSM;
using UnityEngine;

namespace Assets.Scripts.Refactoring.Controller.Enemy.BluePig.State {
    public class BluePigPatrolState : BluePigBaseState {

        private bool isOnTarget;

        public BluePigPatrolState(string animName) : base(animName) {
            isOnTarget = false;
        }

        public override void OnEnter() {
            base.OnEnter();

            core.PlayAnim(Base.Core.EnemyAnimType.Move);
        }

        public override void OnUpdate() {
            base.OnUpdate();

            if (core.DetectPlayer()) {
                ChangeState<BluePigChase>();
                return;
            }

            if (isOnTarget && !IsOnLeft && !IsOnRight) {
                isOnTarget = false;
            }

            if (IsOnLeft && !isOnTarget) {
                isOnTarget = true;
                SetPatrolTarget(false);
                GetState<BluePigIdle>().SetStayTime(EnemyData.stayTime);
                ChangeState<BluePigIdle>();
                return;
            }

            if (IsOnRight && !isOnTarget) {
                isOnTarget = true;
                SetPatrolTarget(true);
                GetState<BluePigIdle>().SetStayTime(EnemyData.stayTime);
                ChangeState<BluePigIdle>();
                return;
            }

            core.MoveToTarget(curTargetPos);
        }

        
    }
}
