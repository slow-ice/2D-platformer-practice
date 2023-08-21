

using UnityEngine;

namespace Assets.Scripts.Refactoring.Event {
    public class EnemyHurtEvent {
        public Transform enmeyTrans;
        public int damage;

        public EnemyHurtEvent(Transform enmeyTrans, int damage) {
            this.enmeyTrans = enmeyTrans;
            this.damage = damage;
        }
    }
}
