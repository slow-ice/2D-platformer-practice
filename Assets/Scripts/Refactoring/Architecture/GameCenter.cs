using Assets.Scripts.Refactoring.Model.Enemy;
using Assets.Scripts.Refactoring.Model.Player;
using Assets.Scripts.Refactoring.System.Battle_System;
using QFramework;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Refactoring.Architecture {
    public class GameCenter : Architecture<GameCenter> {
        protected override void Init() {
            RegisterModel<IPlayerModel>(new PlayerModel());
            RegisterModel<IEnemyModel>(new EnemyModel());

            RegisterSystem<IEnemyBattleSystem>(new EnemyBattleSystem());
            RegisterSystem<IPlayerBattleSystem>(new PlayerBattleSystem());
        }
    }
}