using Assets.Scripts.Refactoring.Controller.Weapon;
using QFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Refactoring.Model.Weapon {
    
    public interface IWeapon {
        void RegisterAttackAction();
        WeaponData_SO weaponData { get; }
    }

    [CreateAssetMenu(fileName = "Sword Data", menuName = "Player/Data/Weapon")]
    public class SwordData_SO : WeaponData_SO {
        [Header("Attack Coyote Time")]
        public float attackCoyoteTime = 0.5f;

        [Header("Move Speed List")]
        public List<float> moveSpeedList = new();
    }
}
