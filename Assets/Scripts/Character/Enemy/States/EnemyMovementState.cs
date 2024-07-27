using UnityEngine;

namespace Assets.Scripts.Character.Enemy.States
{
  public class EnemyMovementState : EnemyBaseState
  {
    private Vector3 _pointToMove;

    public EnemyMovementState(EnemyController enemyController) : base(enemyController) { }

    public override void Enter() {
      // Temporary
      if (CharacterWeaponHandler == null || CharacterWeaponHandler.HasWeapon() == false) {
        CharacterAnimationHandler.MovementAnimation2D();
      }

      _pointToMove = NavMeshService.GetRandomPointOnNavMesh(EnemyController.transform.position);
    }

    public override void Update() {
      AgentMovementService.Move(_pointToMove);
    }

    public override void FixedUpdate() {
      if (CharacterEnemyDetection.GetNearestEnemy() != null) {
        FiniteStateMachine.ChangeState(new EnemyBattleState(EnemyController, CharacterEnemyDetection.GetNearestEnemy()));
      }
    }
  }
}