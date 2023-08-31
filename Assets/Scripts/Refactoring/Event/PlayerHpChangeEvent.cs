

namespace Assets.Scripts.Refactoring.Event {
    internal class PlayerHpChangeEvent {
        public int newHp;
        public int MaxHp;

        public PlayerHpChangeEvent(int newHp, int maxHp) {
            this.newHp = newHp;
            MaxHp = maxHp;
        }
    }
}
