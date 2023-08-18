using Assets.Scripts.Refactoring.Architecture;
using Assets.Scripts.Refactoring.Model.Enemy;
using QFramework;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Refactoring.Controller.Enemy.Base.Core {
    public class EnemyCore : IController {

        public Animator mAnimator { get; private set; }
        public Rigidbody2D mRigidbody { get; private set; }

        private EnemyController mController;
        public EnemyData_SO mEnemyData;

        private Dictionary<EnemyAnimType, int> mAnimDic = new ();

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


        public void SetVelocity(float speed) {
            var curVelo = mRigidbody.velocity;
            curVelo.x = speed;
            mRigidbody.velocity = curVelo;
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
