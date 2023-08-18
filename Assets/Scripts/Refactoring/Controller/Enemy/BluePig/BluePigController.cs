using Assets.Scripts.Refactoring.Controller.Enemy.Base;
using Assets.Scripts.Refactoring.Controller.Enemy.Base.FSM;
using Assets.Scripts.Refactoring.Controller.Enemy.BluePig.State;
using UnityEngine;

namespace Assets.Scripts.Refactoring.Controller.Enemy.BluePig {
    internal class BluePigController : EnemyController {
        protected override EnemyState InitialState => GetState<BluePigIdle>();

        protected override void InitializeFSM() {
            RegisterState<BluePigIdle>(new BluePigIdle("Idle"));
            RegisterState<BluePigPatrolState>(new BluePigPatrolState("Patrol"));
            mStateMachine.OnInit(InitialState);
        }
    }
}
