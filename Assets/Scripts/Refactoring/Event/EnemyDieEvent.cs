
using UnityEngine;

namespace Assets.Scripts.Refactoring.Event {
    public class EnemyDieEvent {
        public Transform transform;
        public EnemyDieEvent(Transform transform) {
            this.transform = transform;
        }
    }
}
