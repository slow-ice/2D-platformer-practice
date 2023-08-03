using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Refactoring.FSM.Player_States {
    public class PlayerFSM : FSMMachine<PlayerStatesEnum> {
        protected PlayerCore core;
        protected PlayerController controller;

        public PlayerFSM SetCore(PlayerCore core) {
            this.core = core;
            return this;
        }

        public PlayerFSM SetController(PlayerController controller) {
            this.controller = controller;
            return this;
        }
    }
}