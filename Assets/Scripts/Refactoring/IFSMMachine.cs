using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Refactoring {
    public interface IFSMMachine<TState> {

        FSMState<TState> LastState { get; set; }

        FSMState<TState> CurrentState { get; set; }

        void TryTransition(TState stateName);

        void ExecutTransition();
        
    }
}