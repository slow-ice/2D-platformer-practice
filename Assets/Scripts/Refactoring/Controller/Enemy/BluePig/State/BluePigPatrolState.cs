
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

            if (IsOnLeft && IsTargetOnLeft()) {
                SetPatrolTarget(false);
                GetState<BluePigIdle>().SetStayTime(EnemyData.stayTime);
                ChangeState<BluePigIdle>();
                return;
            }

            if (IsOnRight && !IsTargetOnLeft()) {
                SetPatrolTarget(true);
                GetState<BluePigIdle>().SetStayTime(EnemyData.stayTime);
                ChangeState<BluePigIdle>();
                return;
            }

            core.MoveToTarget((controller as BluePigController).curTargetPos);
        }

        bool IsTargetOnLeft() {
            return (controller as BluePigController).curTargetPos == (Vector2)(controller as BluePigController).leftPointPos;
        }
    }
}
