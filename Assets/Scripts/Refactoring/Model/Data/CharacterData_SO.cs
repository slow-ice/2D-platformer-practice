using UnityEngine;

namespace Assets.Scripts.Refactoring.Model.Data {
    public class CharacterData_SO : ScriptableObject {
        public enum CharacterType {
            Player, Enemy
        }

        public CharacterType characterType;

        [Header("Check Params")]
        public float GroundCheckRadius;
        public float WallCheckDistance = 0.5f;
    }
}
