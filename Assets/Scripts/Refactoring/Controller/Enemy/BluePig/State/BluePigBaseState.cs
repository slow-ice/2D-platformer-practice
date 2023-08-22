using Assets.Scripts.Refactoring.Controller.Enemy.Base.FSM;
using Assets.Scripts.Refactoring.Controller.Enemy.BluePig;
using Assets.Scripts.Refactoring.Controller.Enemy.BluePig.State;
using UnityEngine;

namespace Assets.Scripts.Refactoring.Controller.Enemy {
    public class BluePigBaseState : EnemyState {

        protected bool IsOnLeft => Vector3.Distance(controller.transform.position,
            ((BluePigController)controller).leftPointPos) < 0.3f;
        protected bool IsOnRight => Vector3.Distance(controller.transform.position,
            ((BluePigController)controller).rightPointPos) < 0.3f;
        protected bool IsAttackOver;

        protected Vector3 curTargetPos;

        public bool IsHeavilyHit;

        public BluePigBaseState(string animName) : base(animName) {
        }

        public override void OnUpdate() {
            base.OnUpdate();

            if (core.IsDead) {
                ChangeState<BluePigDie>();
            }
        }

        public void SetPatrolTarget(bool isMoveToLeft) {
            if (isMoveToLeft) {
                curTargetPos = (controller as BluePigController).leftPointPos;
            }
            else {
                curTargetPos = (controller as BluePigController).rightPointPos;
            }
        }

        public void SetHitTrigger() {
            IsHeavilyHit = true;
        }
    }
}
