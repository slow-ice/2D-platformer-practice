using QFramework;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Refactoring.Model.Player {

    public interface IPlayerModel : IModel { }

    public class PlayerModel : AbstractModel, IPlayerModel {
        PlayerData_SO PlayerData;

        protected override void OnInit() {

        }
    }
}