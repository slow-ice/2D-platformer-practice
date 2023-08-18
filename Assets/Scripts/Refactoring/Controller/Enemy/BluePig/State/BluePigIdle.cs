

namespace Assets.Scripts.Refactoring.Controller.Enemy {
    public class BluePigIdle : BluePigBaseState {
        public BluePigIdle(string animName) : base(animName) {
        }

        public override void OnEnter() {
            base.OnEnter();

            core.SetVelocity(EnemyData.moveSpeed);
            core.PlayAnim(Base.Core.EnemyAnimType.Idle);
        }
    }
}
