

using Assets.Scripts.Refactoring.Controller.Enemy.Base;
using Assets.Scripts.Refactoring.Event;
using Cinemachine;
using QFramework;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Refactoring.Command {
    public class AttackPlayerCommand : AbstractCommand {
        public Transform EnemyTrans;
        private CinemachineVirtualCamera virtualCamera;
        private CinemachineBasicMultiChannelPerlin cameraNoise;

        public AttackPlayerCommand(Transform transform) {
            EnemyTrans = transform;
            virtualCamera = Camera.main.transform.parent.GetComponentInChildren<CinemachineVirtualCamera>();
            cameraNoise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            cameraNoise.m_AmplitudeGain = 0;
            cameraNoise.m_FrequencyGain = 0;
        }

        protected override void OnExecute() {
            Debug.Log("Execute attack command");
            var data = EnemyTrans.GetComponent<EnemyController>().mEnemyData;
            EnemyTrans.GetComponent<EnemyController>().StartCoroutine(CameraShake(0.5f));
            this.SendEvent(new PlayerHurtEvent(data.normalAttackDamage));
        }

        IEnumerator CameraShake(float time) {
            cameraNoise.m_AmplitudeGain = 1;
            cameraNoise.m_FrequencyGain = 3;
            yield return new WaitForSeconds(time);
            cameraNoise.m_AmplitudeGain = 0;
            cameraNoise.m_FrequencyGain = 0;
        }
    }
}
