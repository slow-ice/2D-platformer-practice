using Assets.Scripts.Refactoring.Architecture;
using QFramework;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Refactoring {
    public class PlayerController : MonoBehaviour, IController {

        private Animator mAnimator;
        private Rigidbody2D mRigidbody;

        private void Awake() {

        }

        private void Start() {
            
        }

        public IArchitecture GetArchitecture() {
            return PlatformerArc.Interface;
        }
    }

    public enum PlayerStates { }
}