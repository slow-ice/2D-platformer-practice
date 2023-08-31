
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

        [Header("Weapon Looks")]
        public List<Sprite> weaponSprites = new();

        [Header("Weapon Attack Animation")]
        public List<AnimationClip> weaponAnimations = new();

        [Header("Damage")]
        public int NormalDamage = 1;
        public int HeavyHitDamage = 2;
    }
}
