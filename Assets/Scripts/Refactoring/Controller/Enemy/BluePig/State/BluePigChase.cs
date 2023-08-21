
using Assets.Scripts.Refactoring.Controller.Enemy.BluePig.State;
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
        }

        public override void OnExit() {
            base.OnExit();

            core.SetAnimatorSpeed(1f);
        }

        public override void OnUpdate() {
            base.OnUpdate();

            if (!core.DetectPlayer()) {
                GetState<BluePigIdle>().SetStayTime(EnemyData.stayTime);
                ChangeState<BluePigIdle>();
                return;
            }

            if (Vector3.Distance(controller.transform.position, core.mPlayerTrans.position) > EnemyData.attackRange) {
                core.MoveToTarget(core.mPlayerTrans.position, EnemyData.chaseSpeed);
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
