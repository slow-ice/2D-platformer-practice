using Assets.Scripts.Refactoring.Event;
using Assets.Scripts.Refactoring.Model.Player;
using Cinemachine;
using DG.Tweening;
using QFramework;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Refactoring.Command {
    public class AttackEnemyCommand : AbstractCommand {
        private Transform targetEnemyTrans;

        private CinemachineVirtualCamera virtualCamera;
        CinemachineBasicMultiChannelPerlin cameraNoise;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="transform">Enemy transform</param>
        public AttackEnemyCommand(Transform transform) {
            this.targetEnemyTrans = transform;
            virtualCamera = Camera.main.transform.parent.GetComponentInChildren<CinemachineVirtualCamera>();
            cameraNoise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            cameraNoise.m_AmplitudeGain = 0;
            cameraNoise.m_FrequencyGain = 0;
        }
        protected override void OnExecute() {
            //this.SendEvent(new EnemyHurtEvent(targetTrans, damage, IsHeavyHit));
            var controller = this.GetModel<IPlayerModel>().Controller;
            var weaponData = controller.weaponController.CurrentWeapon;
            var state = controller.StateMachine.CurrentState as PlayerAttackState;
            if (state.IsLastHit()) {
                controller.StartCoroutine(CameraShake(0.5f));
                this.SendEvent(new EnemyHurtEvent(this.targetEnemyTrans, weaponData.HeavyHitDamage, true));
            }
            else {
                this.SendEvent(new EnemyHurtEvent(this.targetEnemyTrans, weaponData.NormalDamage, false));
            }
        }

        IEnumerator CameraShake(float time ) {
            cameraNoise.m_AmplitudeGain = 1;
            cameraNoise.m_FrequencyGain = 2;
            yield return new WaitForSeconds(time);
            cameraNoise.m_AmplitudeGain = 0;
            cameraNoise.m_FrequencyGain = 0;
        }
    }
}
