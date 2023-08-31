using Assets.Scripts.Refactoring.Architecture;
using Assets.Scripts.Refactoring.Event;
using Assets.Scripts.Refactoring.Model.Player;
using QFramework;

using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Refactoring.Controller.UI {
    public class UIController : MonoBehaviour, IController {

        public Image hpBarImage;

        private void Start() {
            this.RegisterEvent<PlayerHpChangeEvent>(e => {
                hpBarImage.fillAmount = (float)e.newHp / e.MaxHp;
            });
        }

        public IArchitecture GetArchitecture() {
            return GameCenter.Interface;
        }
    }
}
