using Assets.Scripts.Refactoring.Event;
using Assets.Scripts.Refactoring.Model.Player;
using QFramework;


namespace Assets.Scripts.Refactoring.System.Battle_System {
    public interface IPlayerBattleSystem : ISystem {
    }

    public class PlayerBattleSystem : AbstractSystem, IPlayerBattleSystem {
        protected override void OnInit() {
            this.RegisterEvent<PlayerAttackEvent>(e => {
                var weaponController = this.GetModel<IPlayerModel>().Controller.weaponController;
                weaponController.PlayAnim(e.attackIndex);
                weaponController.HitBox.enabled = true;
            });

            this.RegisterEvent<PlayerHeavyAttackEvent>(e => {
                e.EnemyController.HeavyHurt();

            });
        }
    }
}
