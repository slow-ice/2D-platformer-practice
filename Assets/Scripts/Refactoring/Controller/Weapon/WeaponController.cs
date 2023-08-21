

using Assets.Scripts.Refactoring.Architecture;
using Assets.Scripts.Refactoring.Model.Weapon;
using QFramework;
using UnityEngine;

namespace Assets.Scripts.Refactoring.Controller.Weapon {
    public class WeaponController : MonoBehaviour, IController {

        public IWeapon CurrentWeapon;
        public WeaponData_SO weaponData;

        public Animator mBaseAnimator;
        public Animator mBodyAnimator;
        private Collider2D HitBox;

        void Awake() {
            mBaseAnimator = transform.GetChild(0).GetComponent<Animator>();
            mBodyAnimator = transform.GetChild(1).GetComponent<Animator>();
            HitBox = GetComponent<Collider2D>();
        }

        void Start() {
            this.RegisterEvent<AttackEvent>(e => {
                PlayAnim(e.attackIndex);
                HitBox.enabled = true;
            });
        }

        /// <summary>
        /// 同时播放base和body的动画
        /// </summary>
        /// <param name="attackIndex">攻击序号</param>
        public void PlayAnim(int attackIndex) {
            var anim = weaponData.animList[attackIndex];
            mBaseAnimator.Play(anim.Item1);
            mBodyAnimator.Play(anim.Item2);
        }

        public void SetWeapon(IWeapon weapon) {
            CurrentWeapon = weapon;
        }

        public IArchitecture GetArchitecture() {
            return GameCenter.Interface;
        }
    }
}
