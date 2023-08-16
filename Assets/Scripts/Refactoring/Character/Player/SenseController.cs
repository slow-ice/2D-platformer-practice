using Assets.Scripts.Refactoring.Architecture;
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

        private PlayerData_SO PlayerData;
        private PlayerController mPlayer;

        private int FacingDirection;

        void Start() {
            GroundCheck = new SenseProperty<Collider2D>(
                () => Physics2D.OverlapCircle(groundCheckTrans.position, PlayerData.GroundCheckRadius, PlayerData.GroundLayer),
                value => value == true
                );

            WallCheck = new SenseProperty<RaycastHit2D>(
                () => Physics2D.Raycast(wallCheckTrans.position, Vector2.right * mPlayer.mCore.FacingDirection,
                    PlayerData.WallCheckDistance, PlayerData.GroundLayer),
                value => value.collider != null
                );

            WallBackCheck = new SenseProperty<RaycastHit2D>(
                () => Physics2D.Raycast(wallCheckTrans.position, Vector2.right * -mPlayer.mCore.FacingDirection,
                    PlayerData.WallCheckDistance, PlayerData.GroundLayer),
                value => value.collider != null
                );

            EdgeCheck = new SenseProperty<RaycastHit2D>(
                () => Physics2D.Raycast(edgeCheckTrans.position, Vector2.right * mPlayer.mCore.FacingDirection,
                    PlayerData.WallCheckDistance, PlayerData.GroundLayer),
                value => value.collider != null
                );
        }

        public SenseController SetPlayer(PlayerController player) {
            mPlayer = player;
            PlayerData = player.PlayerData;
            FacingDirection = player.mCore.FacingDirection;
            return this;
        }

        public IArchitecture GetArchitecture() {
            return GameCenter.Interface;
        }
    }
}