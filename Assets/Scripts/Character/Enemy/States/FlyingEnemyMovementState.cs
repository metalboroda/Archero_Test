using Assets.Scripts.Services.Character;
using UnityEngine;

namespace Assets.Scripts.Character.Enemy.States
{
  public class FlyingEnemyMovementState : FlyingEnemyBaseState
  {
    public FlyingEnemyMovementState(FlyingEnemyController flyingEnemyController) : base(flyingEnemyController) { }

    public override void Enter() {
      EnemyMovementHandler.ResetNavMeshSettings();

      PatrollingMovement();
    }

    public override void Update() {
    }

    public override void FixedUpdate() {
      if (CharacterEnemyDetection.GetNearestEnemy() != null) {
        FiniteStateMachine.ChangeState(new FlyingEnemyBattleState(FlyingEnemyController, CharacterEnemyDetection.GetNearestEnemy()));
      }
    }

    public override void Exit() {
      AgentMovementService.StopMovement();
    }

    private void PatrollingMovement() {
      AgentMovementService.StartMoveTo(false,
        NavMeshService.GetRandomPointOnNavMesh(FlyingEnemyController.transform.position, EnemyMovementHandler.PatrollingRadius),
        EnemyMovementHandler.MinPatrollingIdle, EnemyMovementHandler.MaxIdlePatrolling, PatrollingMovement);
    }
  }
}