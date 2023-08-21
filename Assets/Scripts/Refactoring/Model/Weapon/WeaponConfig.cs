using QFramework;
using System;
using UnityEngine;

namespace Assets.Scripts.Refactoring.Model.Weapon {
    public interface IWeapon {
        void RegisterAttackAction();
        WeaponData_SO weaponData { get; }
    }

}
