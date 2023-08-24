
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Refactoring.Model.Weapon {
    public abstract class WeaponData_SO : ScriptableObject {
        [Header("Attribute")]
        public WeaponType weaponType;
        public enum WeaponType {
            Sword, Bow
        }

        [Header("Animation")]
        public List<TwinAnimList> animList = new ();
        [Serializable]
        public struct TwinAnimList {
            public string baseAnim;
            public string bodyAnim;
        }

        [Header("Damage")]
        public int NormalDamage = 1;
        public int HeavyHitDamage = 2;
    }
}
