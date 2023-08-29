using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Refactoring.Model.Weapon {
    [CreateAssetMenu(fileName = "Sword Data", menuName = "Player/Data/Weapon")]
    public class SwordData_SO : WeaponData_SO {
        [Header("Attack Coyote Time")]
        public float attackCoyoteTime = 0.5f;

        [Header("Move Speed List")]
        public List<float> moveSpeedList = new();
    }
}
