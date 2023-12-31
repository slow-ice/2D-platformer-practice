﻿

using Assets.Scripts.Refactoring.Architecture;
using Assets.Scripts.Refactoring.Command;
using Assets.Scripts.Refactoring.Event;
using Assets.Scripts.Refactoring.Model.Weapon;
using Assets.Scripts.Refactoring.System.Battle_System;
using QFramework;
using UnityEngine;

namespace Assets.Scripts.Refactoring.Controller.Weapon {
    public class WeaponController : MonoBehaviour, IController {

        public WeaponData_SO CurrentWeapon { get; private set; }
        private WeaponData_SO weaponData;

        private Animator mBaseAnimator;
        private Animator mBodyAnimator;
        public BoxCollider2D HitBox { get; private set; }

        public bool HasAttacked { get; private set; } = false;

        void Awake() {
            mBaseAnimator = transform.GetChild(0).GetComponent<Animator>();
            mBodyAnimator = transform.GetChild(1).GetComponent<Animator>();
            HitBox = transform.GetChild(1).GetComponent<BoxCollider2D>();
        }

        void Start() {
            //SetWeapon(GetComponent<IWeapon>());
            gameObject.SetActive(false);
            HitBox.enabled = false;
        }

        void FixedUpdate() {
            if (!HasAttacked && HitBox.isActiveAndEnabled) {
                DetectEnemy();
            }
        }

        public void DetectEnemy() {
            var boxPos = HitBox.offset + new Vector2(transform.position.x, transform.position.y);
            var collision = Physics2D.OverlapBox(boxPos, HitBox.size, 0, LayerMask.GetMask("Enemy"));
            if (collision != null) {

                IDamageable attackTarget = collision.GetComponent<IDamageable>();
                if (attackTarget == null) { 
                    attackTarget = collision.GetComponentInParent<IDamageable>();
                }
                attackTarget?.Hurt(controller => {
                    HasAttacked = true;
                });
            }
        }


        #region Anim Funcs

        /// <summary>
        /// 同时播放base和body的动画
        /// </summary>
        /// <param name="attackIndex">攻击序号</param>
        public void PlayAnim(int attackIndex) {
            gameObject.SetActive(true);
            HitBox.enabled = true;
            //var anim = weaponData.animList[attackIndex];
            //mBaseAnimator.Play(anim.baseAnim);
            //mBodyAnimator.Play(anim.bodyAnim);
        }

        public void StopAnim() {
            HitBox.enabled = false;
            HasAttacked = false;
            gameObject.SetActive(false);
        }

        public bool IsAnimationOver() {
            var animInfo = mBaseAnimator.GetCurrentAnimatorStateInfo(0);
            if (animInfo.normalizedTime >= 0.95f) {
                return true;
            }
            return false;
        }

        #endregion

        //public void SetWeapon(IWeapon weapon) {
        //    CurrentWeapon = weapon;
        //    weaponData = weapon.weaponData;
        //}

        public IArchitecture GetArchitecture() {
            return GameCenter.Interface;
        }
    }
}
