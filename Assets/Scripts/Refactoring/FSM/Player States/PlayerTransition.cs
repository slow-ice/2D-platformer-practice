using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Refactoring.FSM.Player_States {
    public class PlayerTransition : FSMTransition<PlayerStatesEnum> {
        public PlayerTransition(PlayerStatesEnum fromState, PlayerStatesEnum toState) : base(fromState, toState) {
        }
    }
}