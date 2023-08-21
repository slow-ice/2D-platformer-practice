using Assets.Scripts.Refactoring.Command;
using Assets.Scripts.Refactoring.Controller.Enemy.Base;
using Assets.Scripts.Refactoring.Controller.Enemy.Base.Core;
using Assets.Scripts.Refactoring.Controller.Enemy.Base.FSM;
using Assets.Scripts.Refactoring.Controller.Enemy.BluePig.State;
using Assets.Scripts.Refactoring.Model.Enemy;
using QFramework;
using UnityEngine;

namespace Assets.Scripts.Refactoring.Controller.Enemy.BluePig {
    internal class BluePigController : EnemyController {
        protected override EnemyState InitialState => GetState<BluePigIdle>();
        protected override EnemyCore InitialCore => new BluePigCore();

        public Vector3 leftPointPos;
        public Vector3 rightPointPos;

        protected override void Update() {
            base.Update();
            if (Input.GetKeyDown(KeyCode.K)) {
                this.SendCommand(new TryAttackCommand(transform, 1));
            }
        }

        protected override void InitializeFSM() {
            RegisterState<BluePigIdle>(new BluePigIdle("Idle"));
            RegisterState<BluePigPatrolState>(new BluePigPatrolState("Patrol"));
            RegisterState<BluePigChase>(new BluePigChase("Chase"));
            RegisterState<BluePigAttack>(new BluePigAttack("Attack"));

            GetState<BluePigIdle>().SetPatrolTarget(true);
            mStateMachine.OnInit(InitialState);
        }

        public override void InitializeComponent() {
            base.InitializeComponent();

            leftPointPos = transform.Find("LeftEdge").position;
            rightPointPos = transform.Find("RightEdge").position;
            
        }


        private void OnDrawGizmos() {
            Gizmos.DrawWireCube(transform.position, new Vector3(mEnemyData.attackRange, 
                transform.GetComponent<BoxCollider2D>().size.y, 0));
        }
    }
}
