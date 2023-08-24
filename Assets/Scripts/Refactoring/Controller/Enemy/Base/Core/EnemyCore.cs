using Assets.Scripts.Refactoring.Architecture;
using Assets.Scripts.Refactoring.Model.Enemy;
using QFramework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace Assets.Scripts.Refactoring.Controller.Enemy.Base.Core {
    public class EnemyCore : IController {

        public Animator mAnimator { get; private set; }
        public Rigidbody2D mRigidbody { get; private set; }

        protected EnemyController mController;
        public EnemyData_SO mEnemyData;

        private Dictionary<EnemyAnimType, int> mAnimDic = new ();

        public int FacingDirection;

        public bool IsDead = false;
        public bool IsHurt = false;

        public Transform mPlayerTrans { get; private set; }

        #region Anim Funcs

        /// <summary>
        /// 播放动画
        /// </summary>
        /// <param name="type">动画类型</param>
        public void PlayAnim(EnemyAnimType type) {
            mAnimator.Play(GetAnim(type));
        }

        /// <summary>
        /// 注册动画到字典
        /// </summary>
        /// <param name="type">动画类型</param>
        /// <param name="animName">Animator中字符串名字</param>
        public void RegisterAnim(EnemyAnimType type, string animName) {
            mAnimDic.Add(type, Animator.StringToHash(animName));
        }

        public int GetAnim(EnemyAnimType type) { 
            return mAnimDic[type];
        }

        public void SetAnimatorSpeed(float speed) {
            mAnimator.speed = speed;
        }

        public bool IsAnimationOver() {
            var animInfo = mAnimator.GetCurrentAnimatorStateInfo(0);
            return animInfo.normalizedTime > 0.99f;
        }

        #endregion

        public void Die() {
            IsDead = true;
        }

        public void Hurt() {
            IsHurt = true;
        }

        public bool DetectPlayer() {
            var rayInfo = Physics2D.Raycast(mController.transform.position, mController.transform.localScale,
                mEnemyData.detectDistance, LayerMask.GetMask("Player"));
            if (rayInfo.collider != null) {
                mPlayerTrans = rayInfo.collider.transform;
                return true;
            }
            return false;
        }

        private void MoveWithSpeed(float speed) {
            if (speed > 0 && mController.transform.localScale.x < 0) {
                Flip();
            }
            else if (speed < 0 && mController.transform.localScale.x > 0) {
                Flip();
            }
            SetVelocity(speed);
        }

        public void MoveToTarget(Vector3 target) {
            if (target.x > mController.transform.position.x) {
                MoveWithSpeed(mEnemyData.moveSpeed);
            }
            else if (target.x < mController.transform.position.x) {
                MoveWithSpeed(-mEnemyData.moveSpeed);
            }
        }

        public void MoveToTarget(Vector3 target, float speed) {
            if (target.x > mController.transform.position.x) {
                MoveWithSpeed(speed);
            }
            else if (target.x < mController.transform.position.x) {
                MoveWithSpeed(-speed);
            }
        }

        public void SetVelocity(float speed) {
            var curVelo = mRigidbody.velocity;
            curVelo.x = speed;
            mRigidbody.velocity = curVelo;
        }


        public void Flip() {
            var scale = mController.transform.localScale;
            scale.x *= -1;
            mController.transform.localScale = scale;
        }

        #region Init Funcs
        public EnemyCore OnInit() {
            var animList = mEnemyData.animNames;
            foreach (var anim in animList) {
                RegisterAnim(anim.animType, anim.animNames);
            }
            return this;
        }

        public EnemyCore SetController(EnemyController controller) {
            mController = controller;
            return this;
        }

        public EnemyCore SetAnimator(Animator animator) {
            mAnimator = animator;
            return this;
        }

        public EnemyCore SetRigibody(Rigidbody2D rigibody) {
            mRigidbody = rigibody;
            return this;
        }

        public EnemyCore SetData(EnemyData_SO data) {
            mEnemyData = data;
            return this;
        }
        #endregion

        public IArchitecture GetArchitecture() {
            return GameCenter.Interface;
        }
    }

    public enum EnemyAnimType {
        Idle, Move, Attack, Skill, Hurt, Die
    }
}
