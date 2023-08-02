using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Refactoring {
    public interface IFSMMachine<TState> {

        FSMState<TState> ActiveSubState { get; set; }

        bool TryTransition(FSMTransition<TState> transition);

        void ChangeState(TState state);
        
    }
}