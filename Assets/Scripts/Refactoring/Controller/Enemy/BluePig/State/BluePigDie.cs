using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Refactoring.Controller.Enemy.BluePig.State {
    public class BluePigDie : BluePigBaseState {
        public BluePigDie(string animName) : base(animName) {
        }

        public override void OnEnter() {
            base.OnEnter();

            core.PlayAnim(Base.Core.EnemyAnimType.Die);
        }

        public override void OnUpdate() {
            base.OnUpdate();

            if (core.IsAnimationOver()) {
                controller.DestroySelf();
            }
        }
    }
}
