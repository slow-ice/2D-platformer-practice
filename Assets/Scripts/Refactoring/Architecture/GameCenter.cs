using Assets.Scripts.Refactoring.Model.Player;
using QFramework;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Refactoring.Architecture {
    public class GameCenter : Architecture<GameCenter> {
        protected override void Init() {
            RegisterModel(new PlayerModel());
        }
    }
}