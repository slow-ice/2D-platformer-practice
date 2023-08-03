using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Refactoring {
    public class TestHFSM : MonoBehaviour {

        FSMMachine RootFSM;

        public bool eInput = false;
        public bool wInput = false;

        private bool firstChange = true;

        public string state;

        // Use this for initialization
        void Start() {
            RootFSM = new FSMMachine();
            RootFSM.IsRootMachine = true;

            var idleState = new FSMState("idle");
            var walkState = new FSMState("walk");
            var runState = new FSMState("run");

            var normalSubMachine = new FSMMachine();
            var moveSubMachine = new FSMMachine();

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

            var trans1 = new FSMTransition("run", "walk");
            trans1.AddCondition(() => !eInput);

            var trans2 = new FSMTransition("walk", "run");
            trans2.AddCondition(() => eInput);

            var trans3 = new FSMTransition("normal", "move");
            trans3.AddCondition(() => wInput);

            var trans4 = new FSMTransition("move", "normal");
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

            if (firstChange) {
                wInput = true;
                eInput = true;
                firstChange = false;
            }

            state = RootFSM.ActiveSubState.stateType;
        }

        private void FixedUpdate() {
            RootFSM.OnFixedUpdate();
        }
    }
}