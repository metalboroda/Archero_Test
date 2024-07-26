using UnityEngine;

namespace Assets.Scripts.Character.Enemy.States
{
  public class EnemyMovementState : EnemyBaseState
  {
    public EnemyMovementState(EnemyController enemyController) : base(enemyController) { }

    public override void Enter() {
      // Temporary
      if (CharacterWeaponHandler == null || CharacterWeaponHandler.HasWeapon() == false) {
        CharacterAnimationHandler.MovementAnimation2D();
      }
    }

    public override void FixedUpdate() {
      if (CharacterEnemyDetection.GetNearestEnemy() != null) {
        FiniteStateMachine.ChangeState(new EnemyBattleState(EnemyController, CharacterEnemyDetection.GetNearestEnemy()));
      }
    }
  }
}