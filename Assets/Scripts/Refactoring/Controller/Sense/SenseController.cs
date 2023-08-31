using Assets.Scripts.Refactoring.Architecture;
using Assets.Scripts.Refactoring.Model.Data;
using QFramework;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Refactoring {

    public interface ISenseProperty<T> {
        public T Value { get; }
        public bool IsDetected { get; }

        public void Detect();
    }

    public class SenseProperty<T> : ISenseProperty<T> {
        public T Value { get; private set; }

        public bool IsDetected { get; private set; }

        private Action detectAction;

        public SenseProperty(Func<T> detection, Func<T, bool> result) {
            detectAction = () => {
                Value = detection();
                IsDetected = result(Value);
            };
        }

        public void Detect() => detectAction?.Invoke();

        public static implicit operator bool(SenseProperty<T> s) {
            if (s == null) 
                return false;
            s.Detect();
            return s.IsDetected;
        }
    }

    public class SenseController : MonoBehaviour, IController {

        public SenseProperty<Collider2D> GroundCheck;
        public SenseProperty<RaycastHit2D> WallCheck;
        public SenseProperty<RaycastHit2D> WallBackCheck;
        public SenseProperty<RaycastHit2D> EdgeCheck;


        public Transform groundCheckTrans;
        public Transform wallCheckTrans;
        public Transform edgeCheckTrans;

        //private CharacterData_SO CharacterData;
        public CharacterData_SO CharacterData;
        private PlayerController mPlayer;

        private int FacingDirection;

        void Start() {
            GroundCheck = new SenseProperty<Collider2D>(
                () => Physics2D.OverlapCircle(groundCheckTrans.position, CharacterData.GroundCheckRadius, LayerMask.GetMask("Ground")),
                value => value == true
                );

            WallCheck = new SenseProperty<RaycastHit2D>(
                () => Physics2D.Raycast(wallCheckTrans.position, Vector2.right * transform.localScale.x,
                    CharacterData.WallCheckDistance, LayerMask.GetMask("Ground")),
                value => value.collider != null
                );

            WallBackCheck = new SenseProperty<RaycastHit2D>(
                () => Physics2D.Raycast(wallCheckTrans.position, Vector2.right * -transform.localScale.x,
                    CharacterData.WallCheckDistance, LayerMask.GetMask("Ground")),
                value => value.collider != null
                );

            if (edgeCheckTrans != null) {
                EdgeCheck = new SenseProperty<RaycastHit2D>(
                    () => Physics2D.Raycast(edgeCheckTrans.position, Vector2.right * transform.localScale.x,
                        CharacterData.WallCheckDistance, LayerMask.GetMask("Ground")),
                    value => value.collider != null
                    );
            }
        }

        public SenseController SetPlayer(PlayerController player) {
            mPlayer = player;
            CharacterData = player.PlayerData;
            FacingDirection = player.mCore.FacingDirection;
            return this;
        }

        public IArchitecture GetArchitecture() {
            return GameCenter.Interface;
        }
    }
}