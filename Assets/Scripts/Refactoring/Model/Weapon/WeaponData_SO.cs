
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Refactoring.Model.Weapon {
    public class WeaponData_SO : ScriptableObject {
        [Header("Attribute")]
        public WeaponType weaponType;
        public enum WeaponType {
            Sword, Bow
        }

        [Header("Animation")]
        public List<(string, string)> animList = new List<(string, string)> ();

        [Header("Damage")]
        public int Damage;
    }
}
