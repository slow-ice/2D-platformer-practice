using Assets.Scripts.Refactoring.Controller.Weapon;
using UnityEngine;

namespace Assets.Scripts.Refactoring.Model.Weapon {
    public class WeaponSword : MonoBehaviour, IWeapon {
        public WeaponData_SO weaponData { get; set; }

        [SerializeField]
        private WeaponData_SO weaponData_;

        private void Awake() {
            weaponData = weaponData_;
        }

        void Start() {
            weaponData = weaponData_;
            //GetComponent<WeaponController>().SetWeapon(this);
        }

        public void RegisterAttackAction() {

        }
    }
}
