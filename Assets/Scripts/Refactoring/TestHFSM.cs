using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Refactoring {
    public class TestHFSM : MonoBehaviour {

        FSMMachine<string> RootFSM;

        public bool eInput = false;
        public bool wInput = false;

        public string state;

        // Use this for initialization
        void Start() {
            RootFSM = new FSMMachine<string> ();
            RootFSM.IsRootMachine = true;

            var idleState = new FSMState<string>("idle");
            var walkState = new FSMState<string> ("walk");
            var runState = new FSMState<string>("run");

            var normalSubMachine = new FSMMachine<string>();
            var moveSubMachine = new FSMMachine<string>();

            idleState.OnInit(enter => Debug.Log("enter idle"), update => Debug.Log("update idle"),
                exit => Debug.Log("exit idle"));

            walkState.OnInit(enter => Debug.Log("enter walk"), update => Debug.Log("update walk"),
                exit => Debug.Log("exit walk"));

            runState.OnInit(enter => Debug.Log("enter run"), update => Debug.Log("update run"),
                exit => Debug.Log("exit run"));

            //runState.OnInit(enter => { }, update => { },
            //    exit => { });

            //idleState.OnInit(enter => { }, update => { },
            //    exit => { });

            //walkState.OnInit(enter => { }, update => { },
            //    exit => { });


            normalSubMachine.AddState("idle", idleState);
            moveSubMachine.AddState("walk", walkState);
            moveSubMachine.AddState("run", runState);

            RootFSM.AddState("normal", normalSubMachine);
            RootFSM.AddState("move", moveSubMachine);

            var trans1 = new FSMTransition<string>("run", "walk");
            trans1.AddCondition(() => !eInput);

            var trans2 = new FSMTransition<string>("walk", "run");
            trans2.AddCondition(() => eInput);

            var trans3 = new FSMTransition<string>("normal", "move");
            trans3.AddCondition(() => wInput);

            var trans4 = new FSMTransition<string>("move", "normal");
            trans4.AddCondition(() => !wInput);

            RootFSM.AddTransition(trans3);
            RootFSM.AddTransition(trans4);

            moveSubMachine.AddTransition(trans1);
            moveSubMachine.AddTransition(trans2);

            RootFSM.OnInit();
        }

        // Update is called once per frame
        void Update() {
            if (Input.GetKeyDown(KeyCode.W)) {
                wInput = !wInput;
            }
            if (Input.GetKeyDown(KeyCode.E)) {
                eInput = !eInput;
            }

            RootFSM.OnUpdate();

            state = RootFSM.ActiveSubState.stateType;
        }

        private void FixedUpdate() {
            RootFSM.OnFixedUpdate();
        }
    }
}