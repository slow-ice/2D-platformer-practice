
using Assets.Scripts.Refactoring.Controller.Enemy.Base.Core;
using Assets.Scripts.Refactoring.Model.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Refactoring.Model.Enemy {
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Data/Enemy")]
    public class EnemyData_SO : CharacterData_SO {
        [Header("Patrol Points")]
        //[SerializeField]
        //private GameObject LeftPoint;
        //[SerializeField] 
        //private GameObject RightPoint;
        //[HideInInspector]
        //public Transform leftSidePoint => LeftPoint.transform;
        //[HideInInspector]
        //public Transform rightSidePoint => RightPoint.transform;

        [Header("Move Params")]
        public float moveSpeed = 2f;
        public float chaseSpeed = 5f;
        public float chaseAnimSpeed = 2f;
        public float stayTime = 1f;

        [Header("Attack Params")]
        public float attackCoolDown = 0.5f;
        public float detectDistance = 3f;
        public float attackRange = 1f;
        public int normalAttackDamage = 1;
        public int heavyAttackDamage = 2;

        [Header("Attributes")]
        public int maxHealthPoint;

        [Header("Anim Name")]
        public List<AnimParams> animNames = new ();

        [Serializable]
        public struct AnimParams {
            public string animNames;
            public EnemyAnimType animType;
        }
    }
}
