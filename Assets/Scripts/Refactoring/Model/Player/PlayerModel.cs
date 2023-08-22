using Assets.Scripts.Refactoring.Event;
using QFramework;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Refactoring.Model.Player {

    public interface IPlayerModel : IModel {
        PlayerData_SO PlayerData { get; }
        PlayerController Controller { get; }
        Animator Animator { get; }
        Transform Transform { get; }
        Rigidbody2D Rigidbody { get; }
        BindableProperty<int> Health { get; }

        void RegisterPlayer(Transform transform);
    }

    public class PlayerModel : AbstractModel, IPlayerModel {

        public PlayerController Controller { get; private set; }

        public Animator Animator { get; private set; }

        public Transform Transform { get; private set; }

        public Rigidbody2D Rigidbody { get; private set; }

        public BindableProperty<int> Health { get; private set; } = new BindableProperty<int>(10);

        public PlayerData_SO PlayerData { get; private set; }

        protected override void OnInit() {
            Health.Register(newValue => {
                if (newValue <= 0) {
                    this.SendEvent<PlayerDieEvent>();
                }
            });
        }

        public void RegisterPlayer(Transform transform) {
            Transform = transform;
            Controller = transform.GetComponent<PlayerController>();
            Animator = transform.GetComponent<Animator>();
            Rigidbody = transform.GetComponent<Rigidbody2D>();
            PlayerData = Controller.PlayerData;
        }
    }
}