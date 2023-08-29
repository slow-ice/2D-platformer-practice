
using QFramework;
using System;
using UnityEngine;

namespace Assets.Scripts.Refactoring.System.Battle_System {
    public interface IDamageable {
        void Hurt(Action<IController> callback);

        Transform getHitTransform();
    }
}
