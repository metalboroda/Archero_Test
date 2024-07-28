using Assets.Scripts.Services.Character;

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

      EnemyMovementHandler.ResetNavMeshSettings();

      PatrollingMovement();
    }

    public override void Update() {
      CharacterAnimationHandler.MovementValue2D(
        AgentMovementService.GetDirection2D().x,
        AgentMovementService.GetDirection2D().y);
    }

    public override void FixedUpdate() {
      if (CharacterEnemyDetection.GetNearestEnemy() != null) {
        FiniteStateMachine.ChangeState(new EnemyBattleState(EnemyController, CharacterEnemyDetection.GetNearestEnemy()));
      }
    }

    public override void Exit() {
      AgentMovementService.StopMovement();
    }

    private void PatrollingMovement() {
      AgentMovementService.StartMoveTo(false,
        NavMeshService.GetRandomPointOnNavMesh(EnemyController.transform.position, EnemyMovementHandler.PatrollingRadius),
        EnemyMovementHandler.MinPatrollingIdle, EnemyMovementHandler.MaxIdlePatrolling, PatrollingMovement);
    }
  }
}