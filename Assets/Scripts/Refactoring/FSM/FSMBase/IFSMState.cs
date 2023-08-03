using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Refactoring {
    public interface IFSMState<TState> {
        TState stateType { get; }

        void OnInit();

        void OnEnter();

        void OnExit();

        void OnUpdate();

        void OnFixedUpdate();
    }
}