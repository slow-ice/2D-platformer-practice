
using Assets.Scripts.Refactoring.Controller.Enemy.Base.Core;
using Assets.Scripts.Refactoring.Model.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Refactoring.Model.Enemy {
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Data/Enemy")]
    public class EnemyData_SO : CharacterData_SO {
        [Header("Patrol Points")]
        public Transform leftSidePoint;
        public Transform rightSidePoint;

        [Header("Move Params")]
        public float moveSpeed = 2f;
        public float stayTime = 1f;

        [Header("Attack Params")]
        public float attackCoolDown = 0.5f;

        [Header("Attributes")]
        public float maxHealthPoint;

        [Header("Anim Name")]
        public List<AnimParams> animNames = new ();

        [Serializable]
        public struct AnimParams {
            public string animNames;
            public EnemyAnimType animType;
        }
    }
}
