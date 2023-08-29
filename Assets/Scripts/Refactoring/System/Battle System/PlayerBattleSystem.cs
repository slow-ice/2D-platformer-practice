using Assets.Scripts.Refactoring.Event;
using Assets.Scripts.Refactoring.Model.Player;
using Assets.Scripts.Refactoring.System.Input_System;
using QFramework;
using UnityEngine;

namespace Assets.Scripts.Refactoring.System.Battle_System {
    public interface IPlayerBattleSystem : ISystem {
    }

    public class PlayerBattleSystem : AbstractSystem, IPlayerBattleSystem {
        protected override void OnInit() {
            this.RegisterEvent<PlayerHurtEvent>(e => {
                var model = this.GetModel<IPlayerModel>();
                model.Health.Value -= e.damage;
            });

            this.RegisterEvent<PlayerDieEvent>(e => {
                Debug.Log("Start Die");
                InputManager.Instance.DisableInput();

                var controller = this.GetModel<IPlayerModel>().Controller;
                controller.Die();
            });
        }
    }
}
