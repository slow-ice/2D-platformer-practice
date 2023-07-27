using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Refactoring {
    public class Transition<TState> {
        public TState FromState {  get; set; }

        public TState ToState { get; set; }



        public Transition(TState fromState, TState toState) { 
            this.FromState = fromState; 
            this.ToState = toState; 
        }

    }
}