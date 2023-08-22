using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Refactoring.Controller.Enemy.BluePig.State {
    public class BluePigStunState : BluePigBaseState {
        public BluePigStunState(string animName) : base(animName) {
        }

        public override void OnEnter() {
            base.OnEnter();

            core.PlayAnim(Base.Core.EnemyAnimType.Hurt);
        }
    }
}
