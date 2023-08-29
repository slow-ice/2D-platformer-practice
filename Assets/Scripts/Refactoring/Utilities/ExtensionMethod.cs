using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Refactoring.Utilities {
    internal static class ExtensionMethod {
        public static Vector2 realPosition(this Transform enemy) {
            return new Vector2(enemy.position.x, enemy.position.y + 1);
        }
    }
}
