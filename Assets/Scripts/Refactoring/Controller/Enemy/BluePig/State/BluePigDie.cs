using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Refactoring.Controller.Enemy.BluePig.State {
    public class BluePigDie : BluePigBaseState {
        private bool isDead = false;

        public BluePigDie(string animName) : base(animName) {
        }

        public override void OnEnter() {
            base.OnEnter();

            core.PlayAnim(Base.Core.EnemyAnimType.Die);
        }

        public override void OnUpdate() {
            base.OnUpdate();

            if (core.IsAnimationOver() && !isDead) {
                core.mAnimator.speed = 0;
                isDead = true;
                WaitTimeManager.WaitTime(1.5f, () => controller.DestroySelf());
                //controller.DestroySelf();
            }
        }
    }
}
