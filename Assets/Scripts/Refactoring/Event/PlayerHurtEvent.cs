using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Refactoring.Event {
    public class PlayerHurtEvent {
        public int damage;

        public PlayerHurtEvent(int damage) {
            this.damage = damage;
        }
    }
}
