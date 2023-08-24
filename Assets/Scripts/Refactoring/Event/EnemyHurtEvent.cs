

using UnityEngine;

namespace Assets.Scripts.Refactoring.Event {
    public class EnemyHurtEvent {
        public Transform enmeyTrans;
        public int damage;
        public bool IsHeavyHit;

        public EnemyHurtEvent(Transform enmeyTrans, int damage, bool isHeavyHit) {
            this.enmeyTrans = enmeyTrans;
            this.damage = damage;
            IsHeavyHit = isHeavyHit;
        }
    }
}
