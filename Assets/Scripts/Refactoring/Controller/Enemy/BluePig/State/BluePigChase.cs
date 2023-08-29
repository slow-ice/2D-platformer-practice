
using Assets.Scripts.Refactoring.Controller.Enemy.BluePig.State;
using Assets.Scripts.Refactoring.Utilities;
using UnityEngine;
using static UnityEditor.PlayerSettings;

namespace Assets.Scripts.Refactoring.Controller.Enemy.BluePig {
    public class BluePigChase : BluePigBaseState {
        

        public BluePigChase(string animName) : base(animName) {
        }

        public override void OnEnter() {
            base.OnEnter();

            core.PlayAnim(Base.Core.EnemyAnimType.Move);
            core.SetAnimatorSpeed(EnemyData.chaseAnimSpeed);
            core.CheckShouldFlip(core.mPlayerTrans);
        }

        public override void OnExit() {
            base.OnExit();

            core.SetAnimatorSpeed(1f);
        }

        public override void OnUpdate() {
            base.OnUpdate();

            if (!core.DetectPlayer() && !core.DetectPlayerBack()) {
                GetState<BluePigIdle>().SetStayTime(EnemyData.stayTime);
                ChangeState<BluePigIdle>();
                return;
            }

            Debug.DrawRay(controller.transform.realPosition(), 
                new Vector2(controller.transform.localScale.x * EnemyData.attackRange, 0), Color.blue);

            if (Vector3.Distance(controller.transform.realPosition(), core.mPlayerTrans.position) > EnemyData.attackRange) {
                core.PlayAnim(Base.Core.EnemyAnimType.Move);
                core.MoveToTarget(core.mPlayerTrans.position, EnemyData.chaseSpeed);
            }
            else if (!core.IsAttackCoolDown()) {
                core.PlayAnim(Base.Core.EnemyAnimType.Idle);
            }
            else {
                ChangeState<BluePigAttack>();
            }
        }

        void MoveToPlayer() {
            controller.transform.position = Vector3.MoveTowards(controller.transform.position, core.mPlayerTrans.position,
                Time.deltaTime * EnemyData.chaseSpeed);
        }
    }
}
